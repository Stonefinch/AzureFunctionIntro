

module.exports = function (context, myEventHubTrigger) {
    context.log('Node.js eventhub trigger function processed work item', myEventHubTrigger);

    context.log("context: " + JSON.stringify(context));

    context.done();
};