// URL https://moonrise-sc.loca.lt
// public = k9m6gb0fk
// videos = kdfo9kf7r
// Server monitor command "journalctl -fu moonrise_backend"
console.clear();

const express = require('express');
const datastore = require('nedb')
const localtunnel = require('localtunnel');
const bcrypt = require('bcrypt');
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
const logsWebhook = "https://discord.com/api/webhooks/747734429597827123/e5PU_9Oo-QIJLChZHZW59RSfFEIvqzJ_NXzxUrcHLdSJiGW5JfWC8xiG925j0xrcUiRS";
let usedWebhook = mainWebhook;

const privateWebhook = new Webhook(usedWebhook);
const loggingWebhook = new Webhook(logsWebhook);

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
                let displayName = data[i]['DisplayName'];
                let userId = data[i]['UserId'];
                let moonriseKey = data[i]['MoonriseKey'];

                data[i]['DisplayName'] = Buffer.from(displayName).toString('base64');
                data[i]['UserId'] = Buffer.from(userId).toString('base64');
                data[i]['MoonriseKey'] = Buffer.from(moonriseKey).toString('base64');
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

let encryptKeys = 'kas903khjasd0';
app.get('/' + encryptKeys, async function(req, res)
{
    moonrisedb.find({}, async function(err, data)
    {   
        for (i=0; i < data.length; i++)
        {
            let hashedKey = await bcrypt.hash(data[i]['MoonriseKey'], 10);

            console.log(data[i]['DisplayName']);
            console.log(salt);
            console.log(hashedKey);

            moonrisedb.update({_id: data[i]['_id']}, {$set: { MoonriseKey: hashedKey}}, {multi: true});
        }

    });

    res.end();
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

    user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
    user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
    user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();
    
    console.log(JSON.stringify(user));
    moonrisedb.insert(user);
    res.json(user);
});

// Remove user
let removeUser = 'kwe90adsko90';
app.post('/' + removeUser, function(req, res)
{
    let user = req.body;

    user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
    user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
    user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();
    moonrisedb.find({MoonriseKey: user['MoonriseKey']}, function(err, data)
    {
        if (err)
        {
            console.log(err);
            res.end();
        }
        
        moonrisedb.remove({_id: data[0]['_id']}, {}, function(error, numRemoved)
        {
            if (err)
            {
                console.log(error);
                res.end();
            }
        });
    });

})

// Update user
let updateUser = 'k83jdaa-ok3ka'
app.post('/' + updateUser, function(req, res)
{
    let user = req.body;

    user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
    user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
    user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();

    moonrisedb.update({MoonriseKey: user['MoonriseKey']}, {$set: { DisplayName: user['DisplayName']}}, {multi: true});
    moonrisedb.update({MoonriseKey: user['MoonriseKey']}, {$set: { Premium: user['Premium'] }}, {multi: true});
    moonrisedb.update({MoonriseKey: user['MoonriseKey']}, {$set: { Lewd: user['Lewd'] }}, {multi: true});
    console.log(JSON.stringify(user));
    res.json(JSON.stringify({successful: true}));
});

// Report Crasher
let reportcrasher = 'kldsa9sdo2ld';
app.post('/' + reportcrasher, function(req, res)
{
    let potCrasher = req.body;
    console.log(potCrasher);
    potCrasher['DisplayName'] = Buffer.from(potCrasher['DisplayName'], 'base64').toString();
    potCrasher['UserId'] = Buffer.from(potCrasher['UserId'], 'base64').toString();
    potCrasher['AvatarId'] = Buffer.from(potCrasher['AvatarId'], 'base64').toString();
    potCrasher['AvatarAuthor'][0] = Buffer.from(potCrasher['AvatarAuthor'][0], 'base64').toString();
    potCrasher['AvatarAuthor'][1] = Buffer.from(potCrasher['AvatarAuthor'][1], 'base64').toString();
    potCrasher['AvatarUrl'][0] = Buffer.from(potCrasher['AvatarUrl'][0], 'base64').toString();
    potCrasher['AvatarUrl'][1] = Buffer.from(potCrasher['AvatarUrl'][1], 'base64').toString();

    let crasherEmbed = new MessageBuilder()
    .setTitle('Crasher Alert!')
    .setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com')
    .setURL(tunnelUrl)
    .setColor('#00b0f4')
    .addField('Display Name: ', potCrasher['DisplayName'])
    .addField('User ID: ', potCrasher['UserId'])
    .addField('Avatar ID: ', potCrasher['AvatarId'])
    .addField('Avatar Author: ', potCrasher['AvatarAuthor'][0])
    .addField('Avatar Author ID: ', potCrasher['AvatarAuthor'][1])
    .addField('Avatar Link: ', potCrasher['AvatarUrl'][1])
    .setThumbnail('https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setDescription('Someone using Moonrise has reported a potential crasher!')
    .setImage(potCrasher['AvatarUrl'][0])
    .setFooter('Moonrise', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png')
    .setTimestamp();

    privateWebhook.send(crasherEmbed);
    res.end();v
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

// init_tunnel();
