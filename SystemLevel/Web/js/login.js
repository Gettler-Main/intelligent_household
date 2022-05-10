$(function () {
  $("#register").click(function () {
    let password = $("input[name='register_password']").val()
    let rpassword = $("input[name='register_rpassword']").val()
    console.log(password)
    if (password !== rpassword) {
      alert("两次输入的密码不一致，请重新输入")
      $("input[name='register_rpassword']").val() = ""
      $("input[name='register_password']").val() = ""
      return false
    }
    var userId, port
    $.ajax({
      url: "http://47.93.12.205:8080/controlcenter/User/register",
      type: "get",
      dataType: "json",
      data: "username=" + $("input[name='register_username']").val() + "&password=" + password,
      success: function (result) {
        if (result.success) {
          console.log(result)
          userId = result.data
          $.ajax({
            url: "http://47.93.12.205:8080/controlcenter/Port/findFreePort",
            type: "get",
            dataType: "json",
            data: "",
            success: function (result) {
              console.log(result)
              port = result.data
              $.ajax({
                url: "http://47.93.12.205:8080/controlcenter/Port/addport",
                type: "get",
                dataType: "json",
                data: "userid=" + userId + "&num=" + port,
                success: function (result) {
                  console.log(result)
                  alert("注册成功,正在自动登录")
                  location.href = "/home.html?userid=" + userId
                },
                error: function (result) { alert(result.msg) }
              })
            },
            error: function (result) { alert(result.msg) }
          })
        } else {
          alert(result.msg)
        }
      },
      error: function (result) { alert(result.msg) }
    })


  })

  $("#login").click(function () {
    console.log("login")
    let password = $("input[name='login_password']").val()
    $.ajax({
      url: "http://47.93.12.205:8080/controlcenter/User/check",
      type: "get",
      dataType: "json",
      data: "username=" + $("input[name='login_username']").val() + "&password=" + password,
      success: function (result) {
        if (result.data) {
          console.log(result)
          location.href = "/home.html?userid=" + result.data
        }
      },
      error: function (result) {
        alert(result.msg)
        $("input[name='login_username']").val() = ""
        $("input[name='login_password']").val() = ""
      }
    })
  })
})
