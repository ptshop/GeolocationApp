
function initSearchByIpScreen(options) {

    let ipInput = options.container.find(".searchByIpInput");
    let findButton = options.container.find(".searchByIpButton");
    let resultsSection = options.container.find(".searchByIpResultsSection");
    let errorSection = options.container.find(".searchByIpErrorSection");

    findButton.click(function (e) {
        find();
    });

    ipInput.on("keydown", function (e) {
        if (e.key == "Enter") {
            find();
        }
    });

    function find() {

        let ip = ipInput.val().trim();

        if (!ip) {
            showError("IP адрес не указан");
            return;
        }

        if (!ipIsValid(ip)) {
            showError("IP адрес указан в неверном формате");
            return;
        }

        $.getJSON('/ip/location', { ip: ip })
            .done(function (location) {
                renderResults(location);
                resultsSection.show();
                errorSection.hide();
            })
            .fail(function (jqXHR) {
                if (jqXHR.status >= 500) {
                    showError("Ошибка сервера");
                } else if (jqXHR.status >= 400) {
                    showError("Для заданного IP адреса местоположение не найдено");
                }
            });
    }

    function ipIsValid(ip) {
        const ipRegex = /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
        return ipRegex.test(ip);
    }

    function showError(errorMessage) {
        errorSection.text(errorMessage);
        errorSection.show();

        resultsSection.hide();
    }

    function renderResults(location) {
        $.each(location, function (key, val) {
            resultsSection.find("." + key).text(val);
        });
    }
}