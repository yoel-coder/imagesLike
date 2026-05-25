$(() => {
    setInterval(function () {
        updateLikes();
    }, 1000)
    function updateLikes() {
        const id = $('#id').val()
        $.get('/home/GetLikesById', { id },function (likes) {
            $('#likes-count').text(likes)
        })
    }
})