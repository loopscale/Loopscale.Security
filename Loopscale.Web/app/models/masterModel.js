var masterModel = masterModel || function () {
    var self = this;

    self.key = "";
    self.description = "";

    self.update = function (key, desc) {

        self.key = key;
        self.description = desc;
    };
};