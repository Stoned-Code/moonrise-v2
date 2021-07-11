// URL https://moonrise-sc.loca.lt
// public = k9m6gb0fk
// videos = kdfo9kf7r
// Server monitor command "journalctl -fu moonrise_backend"
console.clear();

const express = require('express');
const datastore = require('nedb')
const localtunnel = require('localtunnel');
const app = express();

// Databases
const moonrisedb = new datastore('data/moonrise.db');
const crasherdb = new datastore('data/crashers.db');

moonrisedb.loadDatabase();
crasherdb.loadDatabase();

// Discord Webhooks
const {Webhook, MessageBuilder} = require('discord-webhook-node');

//const publicWebhook = "https://discord.com/api/webhooks/863554237442031626/QYKVVT7MWO-raOLKwzhTdm3OdxcH4Ny72PceLdhi9cbnd4dG_nHFO8NhL2p4j3R5WtAw";
const mainWebhook = "https://discord.com/api/webhooks/801629909495054346/vACrY70mTMxSEQe8SlELRdKHKXGLTjvuKIXydH-yUD0D1rFylOoGjcGZZdMpii_Wssb6";

let usedWebhook = mainWebhook;

const privateWebhook = new Webhook(usedWebhook);
privateWebhook.setUsername("Moonrise V2");
privateWebhook.setAvatar("https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png");
let tunnelUrl = "";
moonrise_port = 8080;

let mainDomain = "moonrise-sc";
let testDomain = "moonrise-sct";

let officialDomain = "moonrise-sc";


// Local Tunnel Stuff
async function init_tunnel() 
{
    let tunnel = await localtunnel({ port: moonrise_port, subdomain: officialDomain});
    let number = 1;

    while (true && number < 10)
    {
        if (tunnel.url.split('.')[0].split('/')[2].startsWith(officialDomain)) break;

        console.log(tunnel.url.split('.')[0].split('/')[2]);
        tunnel.close();

        tunnel = await localtunnel({port:moonrise_port, subdomain: officialDomain + "-" + number.toString()});

        console.log(number);
        number++;
    }

    tunnelUrl = tunnel.url;
    console.log(moonrise_port);
    console.log(tunnel.url);
    let embed = new MessageBuilder()
    .setTitle('Moonrise Backend Started')
    .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
    .setURL(tunnelUrl)
    .setColor('#00b0f4')
    .addField('Tunnel Url: ', tunnelUrl)
    .addField('Port: ', moonrise_port)
    .setThumbnail('https://dl.dropboxusercontent.com/s/urm6d5y2cne0ad2/MoonriseLogo.png')
    .setDescription('Moonrise Backend has launched!')
    .setFooter('Moonrise!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setTimestamp();
    privateWebhook.send(embed);

    tunnel.on('close', function()
    {
        // tunnels are closed
        console.log("Tunnel Closed!")
    });
}

app.use(express.static('public'));
app.use(express.static('files'));
app.use(express.json({limit: '10mb'}));

///////////////////
// Get Requests  //
///////////////////

let moonriseapi = 'kaik23kdsal'; // Gets all users in database.
app.get('/' + moonriseapi, function(req, res)
{
    moonrisedb.find({}, function(err, data)
    {
        if (err)
        {
            res.end();
            return;
        }

        let dbLength = data.length;

        for (var i = 0 ; i < dbLength; i++)
        {
            try
            {
                data[i]['DisplayName'] = Buffer.from(data[i]['DisplayName']).toString('base64');
                data[i]['UserId'] = Buffer.from(data[i]['UserId']).toString('base64');
                data[i]['MoonriseKey'] = Buffer.from(data[i]['MoonriseKey']).toString('base64');
            }
            
            catch (error)
            {
                console.log(error);
            }
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
    let decrypetedKey = Buffer.from(user['MoonriseKey'], 'base64');

    decrypetedKey = decrypetedKey.toString();

    moonrisedb.find({MoonriseKey: decrypetedKey}, function(err, data)
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
            user['UserId'] = Buffer.from(user['UserId'], 'base64');
            user['AvatarUrl'] = Buffer.from(user['AvatarUrl'], 'base64').toString();

            if (user['UserId'] != data[0]['UserId'])
            {
                data[0] = JSON.stringify({isMoonriseUser:false})
            }

            else
            {
                data[0]['isMoonriseUser'] = true;

                let dpn = data[0]['DisplayName'];
                let uid = data[0]['UserId'];
                let mk = data[0]['MoonriseKey'];
                let avaUrl = user['AvatarUrl'];

                console.log(avaUrl);

                let usrEmbed = new MessageBuilder();
                usrEmbed.setTitle('Moonrise');
                usrEmbed.setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com');
                usrEmbed.setURL(tunnelUrl);
                usrEmbed.setThumbnail('https://dl.dropboxusercontent.com/s/urm6d5y2cne0ad2/MoonriseLogo.png');
                usrEmbed.setColor('#00b0f4');
                usrEmbed.addField('Display Name: ', dpn);
                usrEmbed.setDescription('Someone has started using Moonrise!');
                usrEmbed.setImage(avaUrl);
                usrEmbed.setFooter('Moonrise!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png');
                usrEmbed.setTimestamp();
                privateWebhook.send(usrEmbed);

                data[0]['DisplayName'] = Buffer.from(dpn).toString('base64');
                data[0]['UserId'] = Buffer.from(uid).toString('base64');
                data[0]['MoonriseKey'] = Buffer.from(mk).toString('base64');
            }

        }

        delete data[0]['_id'];
        console.log(data[0]);
        res.json(data[0]);
    });
});

// Add user
let adduser = 'k3g5hfdo';
app.post('/' + adduser, function(req, res)
{
    let user = req.body;

    user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64');
    user['UserId'] = Buffer.from(user['UserId'], 'base64');
    user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64');
    
    console.log(user);
    // moonrisedb.insert(user);
    res.json(user);
});

// Report Crasher
let reportcrasher = 'kldsa9sdo2ld';
app.post('/' + reportcrasher, function(req, res)
{
    let potCrasher = req.body;
    console.log(potCrasher);

    let crasherEmbed = new MessageBuilder()
    .setTitle('Crasher Alert!')
    .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
    .setURL(tunnelUrl)
    .setColor('#00b0f4')
    .addField('Display Name: ', potCrasher['DisplayName'])
    .addField('User ID: ', potCrasher['UserId'])
    .addField('Avatar Id: ', potCrasher['AvatarId'])
    .addField('Avatar Author: ', potCrasher['AvatarAuthor'])
    .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setDescription('Someone using Moonrise has reported a potential crasher!')
    .setImage(potCrasher['AvatarUrl'])
    .setFooter('Moonrise', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setTimestamp();

    privateWebhook.send(crasherEmbed);
});

let updateCheck = 'slkefgdga9e3d'
app.post('/' + updateCheck, function(req, res)
{
    let clientInfo = req.body;
});

app.listen(moonrise_port, function()
{
    console.log("Server Listening...");
});

init_tunnel();
