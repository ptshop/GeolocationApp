
function initMainPage() {

    let searchByIpScreen = $(".searchByIpScreen");
    let searchByCityScreen = $(".searchByCityScreen");
    let searchByIpMenuItem = $(".searchByIpMenuItem");
    let searchByCityMenuItem = $(".searchByCityMenuItem");

    searchByIpMenuItem.click(function (event) {
        event.preventDefault();

        searchByCityMenuItem.removeClass("active");
        searchByIpMenuItem.addClass("active");

        searchByCityScreen.hide();
        searchByIpScreen.show();
    });

    searchByCityMenuItem.click(function (event) {
        event.preventDefault();

        searchByIpMenuItem.removeClass("active");
        searchByCityMenuItem.addClass("active");

        searchByIpScreen.hide();
        searchByCityScreen.show();
    });

    initSearchByIpScreen({ container: searchByIpScreen });
    initSearchByCityScreen({ container: searchByCityScreen });
}
