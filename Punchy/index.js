const express = require("express");
const mongoose = require("mongoose");
const api = require("./api");
const port = 5000;
const app = express();

app.use("/api", api);

mongoose.Promise = global.Promise;
mongoose.connect('mongodb://localhost/app');
mongoose.connection.open("open", () => {
    console.log("Connected to database");
    app.listen(port, function () {
        console.log("Web server started on port: " + port)
    });
});