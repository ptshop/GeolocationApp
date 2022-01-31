
function initSearchByCityScreen(options) {

    let cityInput = options.container.find(".searchByCityInput");
    let findButton = options.container.find(".searchByCityButton");
    let resultsSection = options.container.find(".searchByCityResultsSection");
    let errorSection = options.container.find(".searchByCityErrorSection");
    let resultsRowTemplate = options.container.find(".resultsRowTemplate");

    findButton.click(function (e) {
        find();
    });

    cityInput.on("keydown", function (e) {
        if (e.key == "Enter") {
            find();
        }
    });

    function find() {

        let city = cityInput.val()

        if (!city.trim()) {
            showError("Название города не указано");
            return false;
        }

        $.getJSON('/city/locations', { city: city })
            .done(function (locations) {
                renderResults(locations);
                resultsSection.show();
                errorSection.hide();
            })
            .fail(function (jqXHR) {
                if (jqXHR.status >= 500) {
                    showError("Ошибка сервера");
                } else if (jqXHR.status >= 400) {
                    showError("Для заданного названия города местоположения не найдены");
                }
            });
    }

    function showError(errorMessage) {
        errorSection.text(errorMessage);
        errorSection.show();

        resultsSection.hide();
    }

    function renderResults(locations) {
        let tbody = resultsSection.find("table>tbody");
        tbody.empty();

        let template = resultsRowTemplate.text();
        $.each(locations, function (locationKey, locationVal) {
            let newRow = $(template);
            tbody.append(newRow);
            newRow.find(".counter").text(locationKey + 1);
            $.each(locationVal, function (key, val) {
                newRow.find("." + key).text(val);
            });
        });
    }
}
