module.exports = function (context, myEventHubTrigger) {
    context.log('Node.js eventhub trigger function processed work item', myEventHubTrigger);

    context.log("context: " + JSON.stringify(context));
    
    if (myEventHubTrigger.Id) {
        var ticks = (new Date()).getTime();

        var tableResult = {
            PartitionKey: myEventHubTrigger.Id,
            RowKey: ticks,
            Data: myEventHubTrigger
        };

        context.done(null, tableResult);
    }
    else {
        context.log("myEventHubTrigger did not have an id...");
        context.log("myEventHubTrigger: " + JSON.stringify(myEventHubTrigger));

        context.done();
    }
};