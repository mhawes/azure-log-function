# An azure http function to write log documents to a cosmosDB

Learning subjects:
- C#
- Azure Cloud

Description:

This function is designed to allow us to collect Jquery migrate warning logs. This will allow us to collect data generated on a regretion test suite and save us a lot of time and effort while upgrading jquery. 

- Provides an endpoint for a POST request: https://martinslogfunction.azurewebsites.net/api/MartinsLogFunction
- Accepts a json list of log items like so: 
[
    {
        "file": "example1",
        "message": "message1"
    },
    {
        "file": "example2",
        "message": "message2"
    }
]
