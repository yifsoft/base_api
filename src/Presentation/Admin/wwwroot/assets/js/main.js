/*!
   Main Js
*/

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

function convertToSlug(text) {
    var trMap = {
        çÇ: "c",
        ğĞ: "g",
        şŞ: "s",
        üÜ: "u",
        ıİ: "i",
        öÖ: "o",
    };
    for (var key in trMap) {
        text = text.replace(new RegExp("[" + key + "]", "g"), trMap[key]);
    }
    return text
        .replace(/[^-a-zA-Z0-9\s]+/gi, "") // remove non-alphanumeric chars
        .replace(/\s/gi, "-") // convert spaces to dashes
        .replace(/[-]+/gi, "-") // trim repeated dashes
        .toLowerCase();
}


$('[slug-input]').keyup(function () {
    $('[slug-output]').val(convertToSlug($('[slug-input]').val()))
});

$('[meta-tags]').tagify({
    InputValueFormat: valuesArr => valuesArr.map(item => item.value).join(',')
});


