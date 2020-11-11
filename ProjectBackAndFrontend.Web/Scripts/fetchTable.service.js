function fetchTableCustomers(page) {
    var urlFetchingTable = '/GetCustomers';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        page: page,
        element: '#customerTable'
    });
    customerTablePage = page;
}

function fetchTableOrders(page) {
    var urlFetchingTable = '/GetOrders';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        page: page,
        element: '#orderTable'
    });
    orderTablePage = page;
}

function fetchTableProducts(page) {
    var urlFetchingTable = '/Product/GetProducts';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        page: page,
        element: '#productTable'
    });
    productTablePage = page;
}

function fetchTableOffers(page) {
    var urlFetchingTable = '/Product/GetOfferss';
    fetchTable({
        url: urlFetchingTable,
        method: 'GET',
        page: page,
        element: '#offerTable'
    });
    offerTablePage = page;
}