function fetchTableCustomers(params) {
    var urlFetchingTable = '/GetCustomers';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        data: params,
        element: '#customerTable'
    });
    customerTablePage = params.page;
}

function fetchTableOrders(params) {
    var urlFetchingTable = '/GetOrders';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        data: params,
        element: '#orderTable'
    });
    orderTablePage = params.page;
}

function fetchTableProducts(params) {
    var urlFetchingTable = '/Product/GetProducts';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        data: params,
        element: '#productTable'
    });
    productTablePage = params.page;
}

function fetchTableOffers(params) {
    var urlFetchingTable = '/Product/GetOffers';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        data: params,
        element: '#offerTable'
    });
    offerTablePage = params.page;
}