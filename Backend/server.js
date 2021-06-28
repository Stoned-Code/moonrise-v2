// URL https://moonrise-sc.loca.lt
const express = require('express');
const datastore = require('nedb')
const localtunnel = require('localtunnel');
const app = express();
const moonrisedb = new datastore('moonrise.db');
const {Webhook, MessageBuilder} = require('discord-webhook-node');
const hook = new Webhook("https://discord.com/api/webhooks/801629909495054346/vACrY70mTMxSEQe8SlELRdKHKXGLTjvuKIXydH-yUD0D1rFylOoGjcGZZdMpii_Wssb6");
moonrisedb.loadDatabase();
let tunnelUrl = "";
moonrise_port = process.env.PORT || 8080;
console.log(moonrise_port);
// Local Tunnel Stuff
async function init_tunnel() 
{
    let tunnel = await localtunnel({ port: moonrise_port , subdomain: "moonrise-sc-69"});

    tunnelUrl = tunnel.url;
    console.log(tunnel.url);
    let embed = new MessageBuilder()
    .setTitle('Moonrise Backend')
    .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
    .setURL(tunnelUrl)
    .setColor('#00b0f4')
    .addField('Tunnel Url: ', tunnelUrl)
    .addField('Port: ' + moonrise_port)
    .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setDescription('Moonrise Backend has launched!')
    .setImage('https://dl.dropboxusercontent.com/s/ywydbk5lrslk6mg/Jiggle.gif')
    .setFooter('Gotta love titties!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setTimestamp();
    hook.send(embed);

    tunnel.on('close', function()
    {
        let closeembed = new MessageBuilder()
        .setTitle('Moonrise Backend')
        .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
        .setURL(tunnelUrl)
        .setColor('#00b0f4')
        .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
        .setDescription('Moonrise Backend is Closed...')
        .setImage('https://dl.dropboxusercontent.com/s/ywydbk5lrslk6mg/Jiggle.gif')
        .setFooter('Gotta love titties!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
        .setTimestamp();
        hook.send(closeembed);
        // tunnels are closed
        console.log("Tunnel Closed!")
    });
}


app.listen(moonrise_port, function()
{
    
});

app.use(express.static('videos'));
app.use(express.json({limit: '5mb'}));

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
    });
});

app.post('/moonriseuser', function(request, response)
{
    let user = request.body;
    
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