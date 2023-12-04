$('.feature.col-xs-12.col-md-3.col-sm-6').each(function () {
    const $this = $(this);
    const backsideHeight = $this.find('.backside').outerHeight();
    const backsideListHeight = $this.find('.list-inline').outerHeight();

    const height = parseInt(backsideHeight) + parseInt(backsideListHeight);
    const thisHeight = $this.css('height');

    if (height > parseInt(thisHeight)) {
        $this.css('height', height + 'px');
    }
});