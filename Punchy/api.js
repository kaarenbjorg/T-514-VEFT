const express = require("express");
const entities = require("./entities");
const bodyParser = require("body-parser");
const elasticsearch = require("elasticsearch");
const uuid = require("node-uuid");
var sortBy = require('lodash.sortby');
const app = express();

const CONTENT_TYPE = 'application/json';
const ADMIN_TOKEN = "smuuu";

app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(bodyParser.json());

const client = new elasticsearch.Client({
    host : 'localhost:9200',
    log : 'error'
});

//  This method returns a list of all companies that are registered
//  Returns status code 200
//  If something went wrong while fetching
//  the data to the database then status code 500 is returned.
//  GET /api/companies
app.get("/companies", (req, res) => {
    const page = req.query.page || 0;
    const max = req.query.max || 40;
    const search = req.query.search || "";

    var allNames = [];

    const promise = client.search({
        index: 'companies',
        type: 'company',
        size: max,
        from: page,
        body: {
            query: {
                match_all: {}
                /*multi_match: {
                    query: search,
                    type: "phrase_prefix",
                    fields: ['name', 'description']
                }*/
            }
        }
    });

    promise.then((doc) => {
        doc.hits.hits.forEach(function (hit) {
            var fields = {
                id: hit._source.id,
                name: hit._source.name,
                description: hit._source.description // á ekki að vera hér
            }

            allNames.push(fields);
        });

        /*const allNames = doc.hits.hits.map((fields) => {
            return {
                id: fields._id,
                name: fields.name,
            }   
        })*/

        // Held þetta sé ekki að sortera rétt!!
        var newArray = sortBy(allNames, 'name');
        res.status(200).send(allNames);
    }, (err) => {
        res.status(500).send(err.message);
    });
});

//  This method returns a company with the requested id and status code 200.
//  If there is no company with the requested id then status code 404 is returned.
//  If something went wrong while fetching
//  the data to the database then status code 500 is returned.
//  GET api/companies/:id
app.get("/companies/:id", (req, res) => {
    /*var query = {
        _id: req.params.id
    };*/
    const id = req.params.id;

    entities.Company.findById(id, function (err, doc) {
        console.log(id);
        if (err) {
            res.status(500).send(err.message);
            return;
        }
        /*else if (doc.length !== 1) {
            res.status(500).send(err.message);
            return;
        }*/
        else {
            console.log(doc.length);
        }
    });
    
    /*const promise = client.search({
        index: 'companies',
        type: 'company',
        body: {
            query: {
                match: {
                    _id: ID
                }
            }
        }
    });

    promise.then((doc) => {
        const n = doc.hits.hits;
        data = {
            id: n._source.id,
            name: n._source.name,
            punchCount: n._source.punchCount,
            description: n._source.description
        }

        res.send(data);
    }, (err) => {
        res.status(500).send(err.message);
    });*/
});

//  This method adds a new company and returns status code 201. If required properties
//  are missing or if they are in incorrect form then status code 412 is returned.
//  if admin token is missing or is incorrect then status code 401 is returned.
//  POST /api/companies
app.post("/companies", (req, res) => {
    if (req.headers.authorization !== ADMIN_TOKEN) {
        res.status(401).send("Not Authorized");
        return;
    }
     
    if(req.get('Content-Type') !== CONTENT_TYPE) {
        res.status(415).send("Unsupported Media Type");
        return;
    }

    var newCompany = req.body;
    const id = uuid.v1();
    newCompany.id = id;

    var data = {
        id: newCompany.id,
        name: newCompany.name,
        punchCount: newCompany.punchCount,
        description: newCompany.description
    }

    /* Two company can't have the same name */
    entities.Company.find(function (err, n){
        for(var i = 0; i < n.length; i++) {
            if(n[i].name.toUpperCase() === data.name.toUpperCase()) {
                res.status(409).send("Conflict");
                return;
            }
        }
    });

    var entity = new entities.Company(data);

    entity.save(function (err) {
        if (err) {
            res.status(412).send("Payload is not valid");
            return;
        } else {
            client.index({
                index: 'companies',
                type: 'company',
                id: id,
                body: data
            }, function (error) {
                if (error) {
                    res.status(500).send(error.message);
                } else {
                    res.status(201).send({
                        _id: entity._id
                    });
                }
            });                
        }
    });
});

module.exports = app;