var moment = require('moment');

module.exports = function(context, req) {

    var dateish = req.query.date;
    context.log("dateish value: " + dateish);
    context.log("new log line");
    
    var m = moment(dateish);

    if (m.isValid()) {
        var body = "date is valid: " + m.format("YYYY-MM-DDTHH:mm:ss");
        context.res = { status: 200, body: body };
    }
    else {
        var body = "date is not valid.";
        context.res = { status: 200, body: body };
    }

    // note: can also pass response to done()
    context.done();
};