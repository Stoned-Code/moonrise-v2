const express = require('express');
const datastore = require('nedb')
const localtunnel = require('localtunnel');
const app = express();
let moonrisedb = new datastore('moonrise.db');
moonrisedb.loadDatabase();

// Local Tunnel Stuff
async function init_tunnel()
{
    let tunnel = await localtunnel({ port: 4209 , subdomain: "moonrise-sc"});
    // the assigned public url for your tunnel
    // i.e. https://abcdefgjhij.localtunnel.me
    tunnel.url;
    console.log(tunnel.url);
    tunnel.on('close', function()
    {
        // tunnels are closed
        console.log("Tunnel Closed!")
    });
}

app.listen('4209', function()
{
    
});

// app.use(express.static('public'));
app.use(express.json({limit: '5mb'}));

app.post('/moonriseapi', function(request, response)
{
    console.log("I got a request!");

    let data = request.body;

    moonrisedb.insert(data);

    console.log(data);
    response.json(data);
});

app.get('/moonriseapi', function(request, response)
{
    moonrisedb.find({}, function(err, data)
    {
        if (err)
        {
            response.end();
            return;
        }
        response.json(data);
    })

})

app.post('/moonriseuser', function(request, response)
{
    let user = request.body;
    // console.log(user);
    moonrisedb.find({MoonriseKey: user['MoonriseKey']}, function(err, data)
    {

        if (err)
        {
            console.log(err);
            response.send("Error getting information...");
            return;
        }
        console.log(JSON.stringify(data[0]));
        if (data[0] == null)
        {
            data[0] = JSON.stringify({isMoonriseUser:false});
        }

        else
        {
            if (user['UserId'] != data[0]['UserId'])
            {
                data[0] = JSON.stringify({isMoonriseUser:false})
            }

            else
            {
                data[0]['isMoonriseUser'] = true;
            }

        }

        delete data[0]['_id'];
        console.log(data);
        response.json(data);
    });
});

init_tunnel();