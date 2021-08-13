const express = require('express');
const app = express();
const bodyParser = require('body-parser');
const morgan = require('morgan');
const mongoose = require('mongoose');
require('dotenv/config');


//Middelware
app.use(bodyParser.json());
app.use(morgan('tiny'));


//Routes


const productRouter = require('./routers/products');

const api = process.env.API_URL;
app.use(`${api}/products`, productRouter);


//Database connection
mongoose.connect(process.env.CONNECTION_STRING, {
    useNewUrlParser:true,
    useUnifiedTopology:true,
    dbName: 'oshop'
})
.then(()=>{
    console.log('Database connection established');
})
.catch((err)=>{
    console.log(err);
});

//Server
app.listen(3000,  ()=>{
    console.log(api);
    console.log('Server is running on port http://localhost:3000');
});