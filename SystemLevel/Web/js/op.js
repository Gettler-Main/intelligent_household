$(function () {
  if (location.href.indexOf("?") == -1 || location.href.indexOf("=") == -1) {
    return ""
  }
  // 获取链接中参数部分
  var ws
  var queryString = location.href.substring(location.href.indexOf("?") + 1)
  // 分离参数对 ?key=value&key2=value2
  var userid = queryString.split("=")
  console.log(userid)
  var port
  $.ajax({
    url: "http://47.93.12.205:8080/controlcenter/Port/findPortByUserId",
    type: "get",
    dataType: "json",
    data: "userid=" + userid[1],
    success: function (result) {
      console.log(result)
      port = result.data.num
      ws = new WebSocket("ws://47.93.12.205:" + port) //建立连接
      // var ws = new WebSocket("ws://127.0.10.1:50000")   //建立连接
      ws.onopen = function () {
        //发送请求
        console.log("open")
        ws.send("Name-Client")
        // ws.send("AirCondition:OP225")
      }
      ws.onmessage = function (ev) {
        //获取后端响应
        console.log(ev.data)
      }
      ws.addEventListener("message", function (event) {
        var data = event.data
        console.log(data)
        var str = data
        var t = str.indexOf(":") // 传输信息格式为 AirCondition:SY225
        if (t != -1) {
          var tarDevice = str.substring(0, t)
          if (str.Length >= t + 4 && str.Substring(t + 1, t + 3) == "SY") {
            // SY标注是否对客户端操作
            if (str[t + 3] == "0") {
              $("#If" + tarDevice + "On").html("状态：关")
            } else if (str[t + 3] == "1") {
              $("#If" + tarDevice + "On").html("状态：开")
            } else if (str[t + 3] == "2" && str.Length >= t + 6) {
              // SY后为2表示调整温度
              var tem = str.substring(t + 4)
              document.getElementsByClassName(
                tarDevice + " temputer"
              )[0].innerText = tem
            }
          } else if (str.substring(t + 1, t + 6) == "Close") {
            alert(str.substring(0, t) + "连接断开，请在设备端重新连接")
            myDevice = str.substring(0, t)
            if (document.getElementById(myDevice + "State") !== null) {
              document
                .getElementById(myDevice + "State")
                .classList.add("collapsed")
              document
                .getElementById(
                  document
                    .getElementById(myDevice + "State")
                    .getAttribute("aria-controls")
                )
                .classList.remove("show")
              document.getElementById(myDevice + "State").disabled = true
            }
          }
        } else if (str.includes("power")) {
          myDevice = str.substring(5)
          if (document.getElementById(myDevice + "State") !== null) {
            document
              .getElementById(myDevice + "State")
              .classList.remove("collapsed")
            // document
            //   .getElementById(
            //     document
            //       .getElementById(myDevice + "State")
            //       .getAttribute("aria-controls")
            //   )
            //   .classList.add("show")
            document.getElementById(myDevice + "State").disabled = false
          }
        }
        // 处理数据
      })
      ws.onclose = function (ev) {
        console.log("close")
      }
      ws.onerror = function (ev) {
        console.log("error")
      }
    },
    error: function (result) {
      alert("连接失败")
    },
  })

  var devices = [
    "LivingroomAirCondition",
    "LivingroomLight",
    "LivingroomCurtain",
    "KitchenLight",
    "Cooker",
    "BedroomCurtain",
    "BedroomAirCondition",
    "BedroomLight",
    "Strip",
    "Calorifier",
  ]
  var operators = ["up", "down"]
  var deviceSet = new Set(devices)
  var operatorSet = new Set(operators)

  $("input").change(function () {
    let classes = this.className.split(" ")
    let isOp = classes.some((ele) => ele === "OP")
    if (isOp) {
      let device = classes.filter((item) => deviceSet.has(item))
      if (device.length > 0) {
        var thisDevice = device[0]
        if (this.checked) {
          // document.getElementById("#If" + thisDevice + "On").innerText = "状态：开"
          $("#If" + thisDevice + "On").html("状态：开")
          ws.send(thisDevice + ":OP1")
        } else {
          // document.getElementById("#If" + thisDevice + "On").innerText = "状态：关"
          $("#If" + thisDevice + "On").html("状态：关")
          ws.send(thisDevice + ":OP0")
        }
      }
    }
    console.log()
  })

  $("button").click(function () {
    let classes = this.className.split(" ")
    let isOp = classes.some((ele) => ele === "OP")
    if (isOp) {
      let device = classes.filter((item) => deviceSet.has(item))
      if (device.length > 0) {
        var thisDevice = device[0]
        let Operator = classes.filter((item) => operatorSet.has(item))
        console.log(thisDevice)
        if (Operator.length > 0) {
          // var box = document.getElementById('collapseOne')
          // console.log(box.innerText)
          var thisOperator = Operator[0]
          var cur = parseInt(
            document.getElementsByClassName(thisDevice + " temputer")[0]
              .innerText
          )
          if (thisOperator === "up") {
            if (thisDevice.includes("Light")) {
              if (cur == 30) {
              } else {
                cur = cur + 10
              }
            } else {
              cur++
            }
            document.getElementsByClassName(
              thisDevice + " temputer"
            )[0].innerText = cur
          } else {
            if (thisDevice.includes("Light")) {
              if (cur == 10) {
              } else {
                cur = cur - 10
              }
            } else {
              cur--
            }
            document.getElementsByClassName(
              thisDevice + " temputer"
            )[0].innerText = cur
          }
          ws.send(thisDevice + ":OP2" + cur)
        }
      }
    }
    console.log()
  })
  function sleep (time) {
    return new Promise((resolve) => setTimeout(resolve, time))
  }

  $("#nav-profile-tab").click(function () {
    ws.send("getLogs")
    var that = this
    ws.onmessage = function (ev) {
      //获取后端响应
      // console.log(ev.data)
      document.getElementById("logs").innerText = ev.data
    }
  })

  $("#nav-contact-tab").click(function () {
    document.getElementById("port").innerText = "当前用户绑定端口为" + port
  })

  $("#updatePort").click(function () {
    $.ajax({
      url: "http://47.93.12.205:8080/controlcenter/Port/findOutPort",
      type: "get",
      dataType: "json",
      data: "&num=" + $("input[name='newNum']").val(),
      success: function (result) {
        console.log(result)
        if (result.success) {
          $.ajax({
            url: "http://47.93.12.205:8080/controlcenter/Port/updateport",
            type: "get",
            dataType: "json",
            data:
              "num=" +
              port +
              "&userid=" +
              userid[1] +
              "&newnum=" +
              $("input[name='newNum']").val(),
            // port
            // "userid=" +
            // userid[1] +
            // "&newnum=" +
            // $("input[name='newNum']").val() +
            // "&num=" +
            // port,
            success: function (result) {
              console.log(result)
              port = $("input[name='newNum']").val()
              alert("修改成功")
            },
            error: function (result) {
              alert("修改失败")
            },
          })
        } else {
          alert(result.msg)
        }
      },
      error: function (result) {
        alert("修改失败")
      },
    })
  })
})
