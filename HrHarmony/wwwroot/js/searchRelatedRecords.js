const searchableInputsSelector = "input[data-list][data-controller][data-entity-name][data-bs-toggle]";

$(document).ready(() => {
    const $searchableInputs = $(searchableInputsSelector);
    $searchableInputs.click(function () { setListMaxHeight($(this).data("list")) });

    let timer;
    $searchableInputs.on("input", function () {
        const $this = $(this);
        $this.closest(".dropdown").find("input:hidden").val("");

        clearTimeout(timer);
        timer = setTimeout(() => searchRelatedRecords($this), 300);
    });

    $(document).on("click", "input[data-list] + .dropdown-menu .dropdown-item", function () {
        const $this = $(this);
        const selectedText = $this.text();
        const selectedValue = $this.val();
        const $searchInput = $this.closest('.dropdown').find(searchableInputsSelector);
        const $input = $this.closest('.dropdown').find("input:hidden");

        $input.val(selectedValue);
        $searchInput.val(selectedText);
        $searchInput.click();
    });
});

const searchRelatedRecords = ($this) => {
    const listId = $this.data("list");
    const searchTerm = $this.val();
    const controllerName = $this.data("controller");
    const entityName = $this.data("entity-name");
    
    if (searchTerm === "") return;

    $.ajax({
        url: `/${controllerName}/SearchRelatedRecords`,
        method: "GET",
        data: { searchTerm: searchTerm, entityName: entityName },
        success: data => {
            try {
                const $emptyList = $('#' + listId).empty();
                $.each(data, (index, item) => $emptyList.append(`<option class="dropdown-item cursor-pointer" value="${item.value}">${item.text}</option>`));
                setListMaxHeight(listId);
            }
            catch (ex) {
                console.error(ex);
                showAjaxError();
            }
        },
        error: () => showAjaxError()
    });
};

const setListMaxHeight = (listId) => {
    const list = document.getElementById(listId);

    // Oblicz wysokość widoku dostępną dla dropdown
    const windowHeight = window.innerHeight;
    const dropdownOffset = list.getBoundingClientRect().top;
    const availableHeight = windowHeight - dropdownOffset - 20; // Odejmujemy 20 pikseli na margines

    // Ustaw maksymalną wysokość dla listy wyboru
    list.style.maxHeight = availableHeight + "px";
};
