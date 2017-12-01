$(document).ready(function(){
    var del_prod = $(".del_prod");
    for(i = 0; i < del_prod.length; i++)
        {
            $(del_prod[i]).click(function(e){
                e.preventDefault();
                
                $.post('../../components/Helper.php',{controller: 'admin', action: 'deleteProd', data: {id: $(this).data('id')}},showResult);
                location.reload();
            });
        }
});

function showResult(data){
    console.log(data);
}