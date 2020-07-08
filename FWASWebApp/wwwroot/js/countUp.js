window.countUp = () => {
    $('.counter').each(function () {
        let $this = $(this),
            countTo = $this.attr('data-count');
        $({countNum: $this.text()})
            .animate({
                countNum: countTo
            },{
                duration: 500,
                easing: 'swing',
                step: () =>  {
                    $this.text(Math.floor(this.countNum));
                }, complete: () => {
                    $this.text(this.countNum);
                }
            });
    });
};