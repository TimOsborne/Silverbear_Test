(function () {


    function Memory(data) {
        this.id = data.id;
        var mdOrGb = data.size < 1000 ? "MB" : "GB";
        var mdOrGbSize = data.size < 1000 ? data.size : data.size / 1000;
        this.size = ko.observable(mdOrGbSize);
        this.displayName = ko.observable(mdOrGbSize.toString() + " " + mdOrGb)
    }

    function DiskStorage(data) {
        this.id = data.id;
        var gbOrTB = data.size < 1000 ? "GB" : "TB";
        var gbOrTbSize = data.size < 1000 ? data.size : data.size / 1000;
        this.size = ko.observable(data.size);
        this.diskType = ko.observable(data.diskType);
        this.displayName = ko.observable(gbOrTbSize.toString() + " " + gbOrTB + " " + data.diskType);
    }

    function Usbs(data) {
        this.id = data.id;
        this.usbType = ko.observable(data.usbType);
    }

    function Graphics(data) {
        this.id = data.id;
        this.name = ko.observable(data.name);
    }

    function PowerSupply(data) {
        this.id = data.id;
        this.watts = ko.observable(data.watts);
        this.displayName = ko.observable(this.watts() + ' W PSU');
    }

    function Processor(data) {
        this.id = data.id;
        this.name = ko.observable(data.name);
    }

    function PcBuild(data) {
        this.id = data.id;
        this.memory = ko.observable(new Memory(data.memory));
        this.diskStorage = ko.observable(new DiskStorage(data.diskStorage));
        this.usBs = ko.observableArray([]);
        this.usBtext = ko.observable("");
        this.graphics = ko.observable(new Graphics(data.graphics));
        this.weight = ko.observable(data.pcWeight);
        this.powerSupply = ko.observable(new PowerSupply(data.powerSupply));
        this.processors = ko.observable(new Processor(data.processors));
        this.usBsText = ko.observable();

        if (data.usBs.length > 0) {

            var prefex = "";
            for (i = 0; i < data.usBs.length; i++) {
                var x = data.usBs[i];
                this.usBs.push(new UsbCounts(x.id, new Usbs(x.usb), x.numberOf))
                this.usBtext(this.usBtext() + prefex + x.numberOf.toString() + " x " + x.usb.usbType);
                prefex = ", "
            }
        }

    };

    function UsbCounts(id, data, count) {

        this.id = id
        this.usb = data
        this.numberOf = ko.observable(count);

    }

    function AppViewModel() {
        var app = this;

        app.editMode = ko.observable(false);
        app.notEditMode = ko.observable(true);
        app.selectedEdit = ko.observable();
        app.selectedEditIndex = ko.observable();


        app.edit = function (item,event) {

            app.selectDiskStorage(app.diskStorage().filter(x => x.id == item.diskStorage().id)[0]);
            app.selectMemory(app.memory().filter(x => x.id == item.memory().id)[0]);
            app.selectPowerSupply(app.powerSupply().filter(x => x.id == item.powerSupply().id)[0]);
            app.selectGraphics(app.graphics().filter(x => x.id == item.graphics().id)[0]);
            app.selectProcessors(app.processors().filter(x => x.id == item.processors().id)[0]);
            app.weight(item.weight());
            app.selectUsbs(item.usBs());
            var context = ko.contextFor(event.target);
            app.selectedEditIndex(context.$index());
            app.selectedEdit(item);
            app.notEditMode(false);
            app.editMode(true);

        }

        app.saveEdit = function (item) {

            $.ajax("api/PcBuilds/AddBuild", {
                data: ko.toJSON({
                    id: app.selectedEdit().id,
                    memory: app.selectMemory(),
                    diskStorage: app.selectDiskStorage(),
                    usbs: app.selectUsbs(),
                    graphics: app.selectGraphics(),
                    powerSupply: app.selectPowerSupply(),
                    processors: app.selectProcessors(),                    
                    pcWeight: app.weight(),
                }),
                type: "post",
                contentType: "application/json",
                success: function (result) {                   

                    app.selectDiskStorage();
                    app.selectMemory();
                    app.selectPowerSupply();
                    app.selectUsb();
                    app.selectUsbs([]);
                    app.selectGraphics();
                    app.selectProcessors();
                    app.notEditMode(true);
                    app.editMode(false);
                    load();
                }
            });           
        }

        app.pcs = ko.observableArray([]);
            app.diskStorage = ko.observableArray([]);
            app.memory = ko.observableArray([]);
            app.powerSupply = ko.observableArray([]);
            app.usb = ko.observableArray([]);
            app.graphics = ko.observableArray([]);
            app.processors = ko.observableArray([]);
            app.weight = ko.observable();

            app.selectDiskStorage = ko.observable();
            app.selectMemory = ko.observable([]);
            app.selectPowerSupply = ko.observable([]);
            app.selectUsb = ko.observable([]);
            app.selectUsbs = ko.observableArray([]);
            app.selectGraphics = ko.observable([]);
            app.selectProcessors = ko.observable([]);
            
            app.addUsb = function () {
                if (typeof app.selectUsb() === "undefined")
                    return;
                if (app.selectUsbs().filter(x => x.usb.id === app.selectUsb().id).length === 0)
                    app.selectUsbs.push(new UsbCounts(0, app.selectUsb(), 1));
                else {
                    var current = app.selectUsbs().filter(x => x.usb.id === app.selectUsb().id)[0]
                    current.numberOf(current.numberOf() + 1);
                }


        };

            app.removeUsb = function (item,event) {
                item.numberOf(item.numberOf() - 1)
                if (item.numberOf() <= 0) {
                    var context = ko.contextFor(event.target);   
                    app.selectUsbs.splice(context.$index(), 1);

                }
            }
            app.getItem = function (item) {
                return item.usbType;
            }

            app.MemoryControl = function () {
                var self = this;
                self.createNew = new Memory({ id: 0, size: 0 });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.DiskStorageControl = function () {
                var self = this;
                self.createNew = new DiskStorage({ id: 0, size: 0, diskType: "HDD" });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.UsbsControl = function () {
                var self = this;
                self.createNew = new Usbs({ id: 0, usbType: "" });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.GraphicsControl = function () {
                var self = this;
                self.createNew = new Graphics({ id: 0, name: "" });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.PowerSupplyControl = function () {
                var self = this;
                self.createNew = new PowerSupply({ id: 0, watts: 0 });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.ProcessorControl = function () {
                var self = this;
                self.createNew = new Processor({ id: 0, name: "" });
                self.addNew = function () {

                    $.ajax("api/PcBuilds/AddBuild", {
                        data: ko.toJSON(newPc),
                        type: "post",
                        contentType: "application/json",
                        success: function (result) {
                            app.PcBuildView.pcs.push(new PcBuild(result));
                        }
                    });
                };
            }

            app.createNew = function () {

                $.ajax("api/PcBuilds/AddBuild", {
                    data: ko.toJSON({

                        memory: app.selectMemory(),
                        diskStorage: app.selectDiskStorage(),
                        usbs: app.selectUsbs(),
                        graphics: app.selectGraphics(),
                        powerSupply: app.selectPowerSupply(),
                        processors: app.selectProcessors(),
                        pcWeight: app.weight(),

                    }),
                    type: "post",
                    contentType: "application/json",
                    success: function (result) {
                        app.pcs.push(new PcBuild(result));
                    }
                });

            }

            function load() {
                $.getJSON("api/PcBuilds/ActiveBuilds", function (data) {
                    var mappedPcs = $.map(data, function (item) { return new PcBuild(item) });
                    app.pcs(mappedPcs);
                });
            };
            load();

            $.getJSON("api/DiskStorage/all", function (data) {
                var mappedDisks = $.map(data, function (item) { return new DiskStorage(item) });
                app.diskStorage(mappedDisks);
            });

            $.getJSON("api/Graphics/all", function (data) {
                var mappedGraphics = $.map(data, function (item) { return new Graphics(item) });
                app.graphics(mappedGraphics);
            });

            $.getJSON("api/Memory/all", function (data) {
                var mappedMemory = $.map(data, function (item) { return new Memory(item) });
                app.memory(mappedMemory);
            });

            $.getJSON("api/PowerSupply/all", function (data) {
                var mappedPowerSupply = $.map(data, function (item) { return new PowerSupply(item) });
                app.powerSupply(mappedPowerSupply);
            });

            $.getJSON("api/Usb/all", function (data) {
                var mappedUsb = $.map(data, function (item) { return new Usbs(item) });
                app.usb(mappedUsb);
            });

            $.getJSON("api/processor/all", function (data) {
                var mappedProcessors = $.map(data, function (item) { return new Processor(item) });
                app.processors(mappedProcessors);
            });

        }
        vm = new AppViewModel();
        ko.applyBindings(vm);
    } ());

