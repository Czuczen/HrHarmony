$(document).ready(() => {
    $('input[list]').on('input', function () {
        const $this = $(this);
        const listId = $this.attr('list');
        const searchTerm = $this.val();
        const controllerName = $this.data("controller");
        const entityName = $this.data("entity-name");

        $.ajax({
            url: `/${controllerName}/SearchRelatedRecords`,
            method: 'GET',
            data: { searchTerm: searchTerm, entityName: entityName },
            success: data => {
                var dataList = $('#' + listId);
                dataList.empty();

                $.each(data, (index, item) => {
                    dataList.append(`<option data-value="${item.value}">${item.text}</option>`);
                });

                $this.focus();
            },
            error: (error) => {
                console.log(error);
            }
        });
    });

    //$(document).on('input', 'datalist option', function () {
    //    $(this).closest('datalist').siblings('input[list]').val($(this).val());
    //});
});