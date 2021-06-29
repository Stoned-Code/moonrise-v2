// URL https://moonrise-sc.loca.lt
// public = k9m6gb0fk
// videos = kdfo9kf7r
const express = require('express');
const datastore = require('nedb')
const localtunnel = require('localtunnel');
const app = express();
const moonrisedb = new datastore('moonrise.db');
const {Webhook, MessageBuilder} = require('discord-webhook-node');
const hook = new Webhook("https://discord.com/api/webhooks/801629909495054346/vACrY70mTMxSEQe8SlELRdKHKXGLTjvuKIXydH-yUD0D1rFylOoGjcGZZdMpii_Wssb6");
moonrisedb.loadDatabase();
let tunnelUrl = "";
moonrise_port =  4209;

// Local Tunnel Stuff
async function init_tunnel() 
{
    let tunnel = await localtunnel({ port: moonrise_port, subdomain: "moonrise-sc"});
    let number = 1;
    while (true && number < 10)
    {
        if (tunnel.url.split('.')[0].split('/')[2].startsWith("moonrise-sc"))
        {
            break;
        }

        console.log(tunnel.url.split('.')[0].split('/')[2]);
        tunnel.close();

        tunnel = await localtunnel({port:moonrise_port, subdomain: "moonrise-sc-" + number.toString()});

        console.log(number);
        number++;
    }
    tunnelUrl = tunnel.url;
    console.log(moonrise_port);
    console.log(tunnel.url);
    let embed = new MessageBuilder()
    .setTitle('Moonrise Backend')
    .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
    .setURL(tunnelUrl)
    .setColor('#00b0f4')
    .addField('Tunnel Url: ', tunnelUrl)
    .addField('Port: ', moonrise_port)
    .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setDescription('Moonrise Backend has launched!')
    .setImage('https://dl.dropboxusercontent.com/s/ywydbk5lrslk6mg/Jiggle.gif')
    .setFooter('Gotta love titties!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setTimestamp();
    hook.send(embed);

    tunnel.on('close', function()
    {
        // tunnels are closed
        console.log("Tunnel Closed!")
    });
}


app.listen(moonrise_port, function()
{
    
});

app.use(express.static('public'));
app.use(express.static('videos'));
app.use(express.json({limit: '10mb'}));

///////////////////
// Get Requests  //
///////////////////
let moonriseapi = 'kaik23kdsal'
app.get('/' + moonriseapi, function(req, res)
{
    moonrisedb.find({}, function(err, data)
    {
        if (err)
        {
            res.end();
            return;
        }
        res.json(data);
    });
});
let ping = 'md9fjtnj4dm';
app.get('/' + ping, function(req, res)
{
    console.log('Server pinged...');
    res.send(JSON.stringify({foundBackend:true}));
});

////////////////////
// Post Requests  //
////////////////////
// Get Moonrise user info
let moonriseuser = 'ykmhuuvlby';
app.post('/' + moonriseuser, function(req, res)
{
    let user = req.body;
    
    moonrisedb.find({MoonriseKey: user['MoonriseKey']}, function(err, data)
    {

        if (err)
        {
            console.log(err);
            res.send("Error getting information...");
            res.end();
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

                let embed = new MessageBuilder()
                .setTitle('Moonrise')
                .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
                .setURL(tunnelUrl)
                .setColor('#00b0f4')
                .addField('Display Name: ', data[0]['DisplayName'])
                .addField('User ID: ', data[0]['UserId'])
                .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
                .setDescription('Someone has started using moonrise!')
                .setImage(user['AvatarUrl'])
                .setFooter('Gotta love titties!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
                .setTimestamp();
                hook.send(embed);
            }

        }

        delete data[0]['_id'];
        console.log(data);
        res.json(data);
    });
});

// Add user
let adduser = 'k3g5hfdo';
app.post('/' + adduser, function(req, res)
{
    let user = req.body;
    console.log(user);
    moonrisedb.insert(user);
    res.json(user);
});

init_tunnel();