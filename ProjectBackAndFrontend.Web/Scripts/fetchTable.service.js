function fetchTableCustomers(page) {
    var urlFetchingTable = '/GetCustomers';
    fetchTable(urlFetchingTable, 'GET', page, '#panel');
}

function fetchTableOrders(page) {
    var urlFetchingTable = '/GetOrders';
    fetchTable(urlFetchingTable, 'GET', page, '#panel');
}

function fetchTableProducts(page) {
    var urlFetchingTable = '/Product/GetProducts';
    fetchTable(urlFetchingTable, 'GET', page, '#panel');
}

function fetchTableOffers(page) {
    var urlFetchingTable = '/Product/GetOfferss';
    fetchTable(urlFetchingTable, 'GET', page, '#panel');
}