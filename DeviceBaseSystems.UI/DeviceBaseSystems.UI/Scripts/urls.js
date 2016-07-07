
//privateOwnerId = '79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240',
//dataOwnerId = 'DD86E785-7171-498E-A9BB-82E1DBE334EE',
//dataOwnerCenterId = '02313882-9767-446D-B4CE-54004EF0AAC4',
//url_server = "http://217.218.53.71:5050/";

privateOwnerId = '79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240',
dataOwnerId = '79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240',
dataOwnerCenterId = '3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C',

url_server = "http://localhost:59825";

url_prefix = "/api/device/";

urls = {
    loginUrl: url_server  + '/oauth/token',

    //--------------------------------------------------------
    // DeviceModel
    //--------------------------------------------------------
    loaddevicemodel: url_server + url_prefix + "devmdl/ldlst",    
    savedevicemodel: url_server + url_prefix + "devmdl/save", 
    getdevicemodel: url_server + url_prefix + "devmdl/byid",
    removedevicemodel: url_server + url_prefix + "devmdl/rmv",

    //--------------------------------------------------------
    // Brand
    //--------------------------------------------------------
    loadbrand: url_server + url_prefix + "brnd/ldlst",
    savebrand: url_server + url_prefix + "brnd/save",
    getbrand: url_server + url_prefix + "brnd/byid",
    removebrand: url_server + url_prefix + "brnd/rmv",

    //--------------------------------------------------------
    // Company
    //--------------------------------------------------------
    loadcompany: url_server + url_prefix + "company/ldlst",
    savecompany: url_server + url_prefix + "company/save",
    getcompany: url_server + url_prefix + "company/byid",
    removecompany: url_server + url_prefix + "company/rmv",

    //--------------------------------------------------------
    // Company Device
    //--------------------------------------------------------
    loadcompanydevice: url_server + url_prefix + "companydev/ldlst",
    savecompanydevice: url_server + url_prefix + "companydev/save",
    getcompanydevice: url_server + url_prefix + "companydev/byid",
    removecompanydevice: url_server + url_prefix + "companydev/rmv",
};
