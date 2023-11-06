document.addEventListener("DOMContentLoaded", () => {
    const pageSize = $('#pageSize').val();
    const pageNumber = $('input[name="PageNumber"]').val();
    const orderBy = $('input[name="OrderBy"]').val();
    const isDescending = $('input[name="IsDescending"]').val().toLowerCase();
    
    $('a[id^="pageHref"]').click(function () {
        const searchVal = $("#searchInput").val();
        const newPageNumber = $(this).data("page-number")
        const queryString = "?PageNumber=" + newPageNumber + "&PageSize=" + pageSize + "&OrderBy=" + orderBy + "&IsDescending=" + isDescending + "&searchString=" + searchVal;
        window.location.href = window.location.pathname + queryString;
    });

    $('a[id^="columnHref"]').click(function () {
        const searchVal = $("#searchInput").val();
        const newOrderBy = $(this).data("column-name")
        const isDesc = (orderBy === newOrderBy ? !(isDescending === "true") : "false");
        const queryString = "?PageNumber=" + pageNumber + "&PageSize=" + pageSize + "&OrderBy=" + newOrderBy + "&IsDescending=" + isDesc + "&searchString=" + searchVal;
        window.location.href = window.location.pathname + queryString;
    });
});
