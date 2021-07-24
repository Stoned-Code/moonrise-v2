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
const modInfo = new datastore('data/modInfo.db');
const admindb = new datastore('data/admindb');

moonrisedb.loadDatabase();
crasherdb.loadDatabase();
modInfo.loadDatabase();
admindb.loadDatabase();

// Discord Webhooks
const {Webhook, MessageBuilder} = require('discord-webhook-node');


const mainWebhook = "https://discord.com/api/webhooks/801629909495054346/vACrY70mTMxSEQe8SlELRdKHKXGLTjvuKIXydH-yUD0D1rFylOoGjcGZZdMpii_Wssb6";
const logsWebhook = "https://discord.com/api/webhooks/747734429597827123/e5PU_9Oo-QIJLChZHZW59RSfFEIvqzJ_NXzxUrcHLdSJiGW5JfWC8xiG925j0xrcUiRS";
let usedWebhook = mainWebhook;

let publicWebhook = "https://discord.com/api/webhooks/753645492927463554/dbNag3stRbODU4ISoRCKBTfS-r_gkcKINJJEk3kcSeOJW1KTPrPl17bEMCPb-2oLaez3";
publicWebhook = new Webhook(publicWebhook);
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

////////////////////
// Post Requests  //
////////////////////

// Get Moonrise user info
let moonriseuser = 'ykmhuuvlby';
app.post('/' + moonriseuser, async function(req, res)
{
    let user = req.body;
    let decrypetedKey = Buffer.from(user['MoonriseKey'], 'base64').toString();

    decrypetedKey = decrypetedKey.toString();
    try
    {
        moonrisedb.find({ UserId: Buffer.from(user['UserId'], 'base64').toString() }, async function(err, data)
        {
            if (err)
            {
                console.log(err);
                res.send("Error getting information...");
                res.end();
                return;
            }
    
            if (data[0] == null)
            {
                res.send("Denied access... Not in Moonrise database.");
                res.end();
                return;
            }
    
            else
            {
                try
                {
                    console.log(JSON.stringify(data[0]));
                    user['UserId'] = Buffer.from(user['UserId'], 'base64');
                    user['AvatarUrl'] = Buffer.from(user['AvatarUrl'], 'base64').toString();

                    if (decrypetedKey == Buffer.from(data[0]['MoonriseKey'], 'base64'))
                    {
                        if (user['UserId'] != data[0]['UserId'])
                        {
                            res.send("Denied access... Key Doesn't Match.");
                        }
            
                        else
                        {
                            data[0]['isMoonriseUser'] = true;

                            console.log(user['AvatarUrl']);
            
                            let usrEmbed = new MessageBuilder();
                            usrEmbed.setTitle('Moonrise');
                            usrEmbed.setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com');
                            usrEmbed.setURL(tunnelUrl);
                            usrEmbed.setThumbnail('https://dl.dropboxusercontent.com/s/urm6d5y2cne0ad2/MoonriseLogo.png');
                            usrEmbed.setColor('#00b0f4');
                            usrEmbed.addField('Display Name: ', data[0]['DisplayName']);
                            usrEmbed.setDescription('Someone has started using Moonrise!');
                            usrEmbed.setImage(user['AvatarUrl']);
                            usrEmbed.setFooter('Moonrise!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png');
                            usrEmbed.setTimestamp();
                            privateWebhook.send(usrEmbed);
            
                            data[0]['DisplayName'] = Buffer.from(data[0]['DisplayName']).toString('base64');
                            data[0]['UserId'] = Buffer.from(data[0]['UserId']).toString('base64');
                            data[0]['MoonriseKey'] = Buffer.from(decrypetedKey).toString('base64');
    
                            delete data[0]['_id'];
                            console.log(data[0]);
                            res.json(data[0]);
                        }
                    }
    
                    else
                    {
                        res.send("Denied access... Wrong Key...");
                    }
                }
    
                catch
                {
                    res.status(500).send();
                }
            }
        });
    }

    catch
    {
        res.status(500).send();
    }
});

// Add admin
let addAdmin = 'lksiodf9kalko233dls';
app.post('/' + addAdmin, async function(req, res)
{
    let admin = req.body;

    
});

// // Add user
// let adduser = 'k3g5hfdo';
// app.post('/' + adduser, async function(req, res)
// {
//     let user = req.body;

//     user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
//     user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
//     user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();
    
//     user['MoonriseKey'] = Buffer.from(user['MoonriseKey']).toString('base64');
    
//     console.log(JSON.stringify(user));
//     moonrisedb.insert(user);
//     res.json(user);
// });

// // Remove user
// let removeUser = 'kwe90adsko90';
// app.post('/' + removeUser, function(req, res)
// {
//     let user = req.body;

//     user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
//     user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
//     user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();
//     moonrisedb.find({MoonriseKey: user['MoonriseKey']}, function(err, data)
//     {
//         if (err)
//         {
//             console.log(err);
//             res.end();
//         }
        
//         moonrisedb.remove({_id: data[0]['_id']}, {}, function(error, numRemoved)
//         {
//             if (err)
//             {
//                 console.log(error);
//                 res.end();
//             }
//         });
//     });

// })

// // Update user
// let updateUser = 'k83jdaa-ok3ka'
// app.post('/' + updateUser, function(req, res)
// {
//     let user = req.body;

//     user['DisplayName'] = Buffer.from(user['DisplayName'], 'base64').toString();
//     user['UserId'] = Buffer.from(user['UserId'], 'base64').toString();
//     user['MoonriseKey'] = Buffer.from(user['MoonriseKey'], 'base64').toString();

//     moonrisedb.update({MoonriseKey: Buffer.from(user['MoonriseKey']).toString('base64')}, {$set: { DisplayName: user['DisplayName']}}, {multi: true});
//     moonrisedb.update({MoonriseKey: Buffer.from(user['MoonriseKey']).toString('base64')}, {$set: { Premium: user['Premium'] }}, {multi: true});
//     moonrisedb.update({MoonriseKey: Buffer.from(user['MoonriseKey']).toString('base64')}, {$set: { Lewd: user['Lewd'] }}, {multi: true});
//     console.log(JSON.stringify(user));
//     res.json(JSON.stringify({successful: true}));
// });

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

// Check mod version
let updateCheck = 'slkefgdga9e3d'
app.post('/' + updateCheck, function(req, res)
{
    console.log("Checking udpate...");
    //const updateLink = Buffer.from().toString('base64');
    let clientInfo = req.body;
    console.log(clientInfo);
    try
    {
        modInfo.find({mod: "MoonriseV2"}, function(err, data)
        {
            if (err)
            {
                console.log(err);
                res.end();
            }

            try
            {
                if (clientInfo['modBuild'] < data[0]['modBuild'])
                {
                    delete data[0]['_id'];
                    delete data[0]['mod']
                    try
                    {
                        data[0]['downloadLink'] = Buffer.from(data[0]['downloadLink']).toString('base64');
                    }
                    catch {}
                    
                    if (data[0]['modChanges'].length > 0)
                        for (i=0; i < data[0]['modChanges'].length; i++)
                        {
                            data[0]['modChanges'][i] = Buffer.from(data[0]['modChanges'][i]).toString('base64');
                        }
        
                    res.json(data[0]);
                }
        
                else
                {
                    res.send("Up to date!");
                }
            }

            catch
            {
                res.status(500).send();
            }
        });
    }

    catch
    {
        res.status(500).send();
    }
});

// Push update
let pushUpdate = 'la03dgadsg0923ioasdf'
app.post('/' + pushUpdate, function(req, res)
{
    let modinf = req.body;
    console.log(modinf);
    modinf['modBuild'] = parseInt(Buffer.from(modinf['modBuild'], 'base64').toString());
    if (modinf['downloadLink'] != null)
        modinf['downloadLink'] = Buffer.from(modinf['downloadLink'], 'base64').toString();
    console.log(modinf);

    let changes = "";
    for (let i = 0; i < modinf['modChanges'].length; i++)
    {
        let change = Buffer.from(modinf['modChanges'][i], 'base64').toString();
        if (change.startsWith("##") == false)
            changes += modinf['modChanges'][i] + '\n';
    }
    console.log(modinf);
    try
    {
        modInfo.update({mod: 'MoonriseV2'}, {$set: { modBuild: modinf['modBuild']}}, multi=true);
        modInfo.update({mod: 'MoonriseV2'}, {$set: {downloadLink: modinf['downloadLink']}}, multi=true);
        modInfo.update({mod: 'MoonriseV2'}, {$set: { modChanges: modinf['modChanges']}}, multi=true);
    
        if (!debug)
        {
            let usrEmbed = new MessageBuilder();
            usrEmbed.setTitle('Moonrise');
            usrEmbed.setAuthor('Stoned Code', 'https://dl.dropboxusercontent.com/s/fnp0bv76c99ve65/UshioSmokingRounded.png', 'https://stoned-code.com');
            // usrEmbed.setURL(tunnelUrl);
            usrEmbed.setThumbnail('https://dl.dropboxusercontent.com/s/urm6d5y2cne0ad2/MoonriseLogo.png');
            usrEmbed.setColor('#00b0f4');
            usrEmbed.addField('Build:', modinf['modBuild']);
            usrEmbed.addField('Changes:', changes);
            usrEmbed.setDescription('Update Available for Moonrise!');
            // usrEmbed.setImage();
            usrEmbed.setFooter('Moonrise Update!', 'https://dl.dropboxusercontent.com/s/jq77qx0on9mnir4/MisheIcon.png');
            usrEmbed.setTimestamp();
            publicWebhook.send(usrEmbed);
        }
    }

    catch
    {
        modinf['mod'] = "MoonriseV2";
        modInfo.insert(modinf);
        console.log("Error updating mod info...");
    }

    res.send("Successful!");
})

app.listen(moonrise_port, function()
{
    console.log("Server Listening...");
});

init_tunnel();
