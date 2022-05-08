$(function () {
  var ws = new WebSocket("ws://47.93.12.205:50000"); //建立连接
  // var ws = new WebSocket("ws://127.0.10.1:50000")   //建立连接
  ws.onopen = function () {
    //发送请求
    console.log("open");
    ws.send("Name-Client");
    // ws.send("AirCondition:OP225")
  };
  ws.onmessage = function (ev) {
    //获取后端响应
    console.log(ev);
  };
  ws.onclose = function (ev) {
    console.log("close");
  };
  ws.onerror = function (ev) {
    console.log("error");
  };

  var devices = ["AirCondition", "BedroomLight", "Calorifier"];
  var operators = ["up", "down"];
  var deviceSet = new Set(devices);
  var operatorSet = new Set(operators);

  $("input").change(function () {
    let classes = this.className.split(" ");
    let isOp = classes.some((ele) => ele === "OP");
    if (isOp) {
      let device = classes.filter((item) => deviceSet.has(item));
      if (device.length > 0) {
        var thisDevice = device[0];
        if (this.checked) {
          ws.send(thisDevice + ":OP1");
        } else {
          ws.send(thisDevice + ":OP0");
        }
      }
    }
    console.log();
  });

  $("button").click(function () {
    let classes = this.className.split(" ");
    let isOp = classes.some((ele) => ele === "OP");
    if (isOp) {
      let device = classes.filter((item) => deviceSet.has(item));
      if (device.length > 0) {
        var thisDevice = device[0];
        let Operator = classes.filter((item) => operatorSet.has(item));
        console.log(thisDevice);
        if (Operator.length > 0) {
          // var box = document.getElementById('collapseOne')
          // console.log(box.innerText)
          var thisOperator = Operator[0];
          var cur = parseInt(
            document.getElementsByClassName(thisDevice + " temputer")[0]
              .innerText
          );
          if (thisOperator === "up") {
            cur++;
            document.getElementsByClassName(
              thisDevice + " temputer"
            )[0].innerText = cur;
          } else {
            cur--;
            document.getElementsByClassName(
              thisDevice + " temputer"
            )[0].innerText = cur;
          }
          ws.send(thisDevice + ":OP2" + cur);
        }
      }
    }
    console.log();
  });
  function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
  }

  $("#nav-profile-tab").click(function () {
    ws.send("getLogs");
    var that = this;
    ws.onmessage = function (ev) {
      //获取后端响应
      console.log(ev.data);
      document.getElementById("logs").innerText = ev.data;
    };
  });
});
