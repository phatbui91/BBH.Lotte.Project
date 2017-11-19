function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(emailAddress);
};

function CheckPassword(str) {
    accents_arr = new Array(
    "à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ", "ẫ", "ă",
    "ằ", "ắ", "ặ", "ẳ", "ẵ", "è", "é", "ẹ", "ẻ", "ẽ", "ê", "ề",
    "ế", "ệ", "ể", "ễ",
    "ì", "í", "ị", "ỉ", "ĩ",
    "ò", "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ",
    "ờ", "ớ", "ợ", "ở", "ỡ",
    "ù", "ú", "ụ", "ủ", "ũ", "ư", "ừ", "ứ", "ự", "ử", "ữ",
    "ỳ", "ý", "ỵ", "ỷ", "ỹ",
    "đ",
    "À", "Á", "Ạ", "Ả", "Ã", "Â", "Ầ", "Ấ", "Ậ", "Ẩ", "Ẫ", "Ă",
    "Ằ", "Ắ", "Ặ", "Ẳ", "Ẵ",
    "È", "É", "Ẹ", "Ẻ", "Ẽ", "Ê", "Ề", "Ế", "Ệ", "Ể", "Ễ",
    "Ì", "Í", "Ị", "Ỉ", "Ĩ",
    "Ò", "Ó", "Ọ", "Ỏ", "Õ", "Ô", "Ồ", "Ố", "Ộ", "Ổ", "Ỗ", "Ơ",
    "Ờ", "Ớ", "Ợ", "Ở", "Ỡ",
    "Ù", "Ú", "Ụ", "Ủ", "Ũ", "Ư", "Ừ", "Ứ", "Ự", "Ử", "Ữ",
    "Ỳ", "Ý", "Ỵ", "Ỷ", "Ỹ",
    "Đ"
);
    //for (var i = 0, n = str.length; i < n; i++) {
    //    if (str.charCodeAt(i) > 255) { return true; }
    //}
    for (var j = 0; j < accents_arr.length; j++) {
        if (str.indexOf(accents_arr[j]) != -1) {
            return true;
        }
    }
    return false;
}

function ResetForm(id) {
    switch (id) {
        case 1:
            {
                $('#lbEmail').text('');
                break;
            }
       
        case 2:
            {
                $('#lbPass').text('');
                break;
            }
    }
}

function MemberLogin() {

    var email = $('#txtEmail').val();
    var pass = $('#txtPassword').val();
    var checkpass = CheckPassword(pass);
    var checkemail = isValidEmailAddress(email);

    var check = true;
 
    if(email=='')
    {
        check=false;
        $('#divMessage').text('Please input username and password');
    }

    else{
        if (!isValidEmailAddress(email)) {
            check = false;
            $('#divMessage').text('email not math');
        }
    }

    if (pass == '')
    {
        check = false;
        $('#divMessage').text('Please input username and password');
    }

    if(!check)
    {
        return false;
    }

    else 
    {
        $.ajax({
            type:"POST",
            url: "/Login/MemberLogin",
            data: { email: email, pass: pass },
            async: true,
            beforeSend: function () { },
            success: function (d) {
                if(d =='loginfaile')
                {
                    $('#divMessage').text('Username or password is wrong');
                    alert("email not corecc");
                }
                else if(d=='notExit')
                {
                    $('#divMessage').text('Username or password not math');
                }
                else if (d == 'loginSuccess') {

                    window.location.reload();
                }
            },
            error: function () {
            },
        });
    }
}

function SubmitToken()
{
    var email = $('#txtEmailToken').val();
    var checkemail = isValidEmailAddress(email);

    var check = true;

    if (email == '') {
        check = false;
        $('#divMessagetoken').text('Please input username and password');
    }

    else{
        if (!isValidEmailAddress(email)) {
            check = false;
            $('#divMessagetoken').text('email not math');
        }
    }

    if (!check) {
        return false;
    }

    else {
        $.ajax({
            type: "POST",
            url: "/Login/SubmitToken",
            data: { email: email},
            async: true,
            beforeSend: function () { },
            success: function (d) {
                if (d == 'loginfaile') {
                    alert("string token faile");
                }
                else if (d == 'loginSuccess') {
                    alert("string toke is math");
                }
            },
            error: function () {
            },
        });
    }
}
function EnterLogin(event, value) {
    if (event.which == 13 || event.keyCode == 13) {
        var email = $('#txtEmail').val();
        var pass = $('#txtPassword').val();
        var checkpass = CheckPassword(pass);
        var checkemail = isValidEmailAddress(email);

        var check = true;

        if (!checkemail) {
            $('divMessage').text('email not math');
        }
        else if (email == '') {
            check = false;
            $('#divMessage').text('Please input username and password');
        }

        if (pass == '') {
            check = false;
            $('#divMessage').text('Please input username and password');
        }

        else if (check) {
            $.ajax({
                type: "POST",
                url: "Login/MemberLogin",
                data: { email: email, pass: pass},
                async: true,
                beforeSend: function () { },
                sucess: function (d) {
                    if (d == 'loginfaile') {
                        $('#divMessage').text('Username or password is wrong');
                    }
                    else if (d == 'loginSuccess') {
                        window.location.href("/");
                    }
                },
                error: function () {
                },
            });
        }
    }
}