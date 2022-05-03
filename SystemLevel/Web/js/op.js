
$(function () {
    var ws = new WebSocket("ws://127.0.0.1:50000")   //建立连接
    ws.onopen = function () {  //发送请求
        console.log("open")
        ws.send("Name-Client")
        ws.send("AirCondition:OP225")
    }
    ws.onmessage = function (ev) {  //获取后端响应
        console.log(ev)
    }
    ws.onclose = function (ev) {
        console.log("close")
    }
    ws.onerror = function (ev) {
        console.log("error")
    }

    

    var devices = ['AirCondition', 'BedroomLight', 'Calorifier']
    var operators = ['up', 'down']
    var deviceSet = new Set(devices)
    var operatorSet = new Set(operators)
    $("button").click(function () {
        sleep(5000).then(() => {
            ws.send("Name-Client")
            ws.send("AirCondition:OP225")
        })

        let classes = this.className.split(' ')
        let isOp = classes.some(ele => ele === 'OP')
        if (isOp) {
            let device = classes.filter(item => deviceSet.has(item))
            if (device.length > 0) {
                var thisDevice = device[0]
                let Operator = classes.filter(item => operatorSet.has(item))
                console.log(thisDevice)
                if (Operator.length > 0) {
                    var thisOperator = Operator[0]
                    console.log(thisOperator)
                }
            }
        };
        console.log()

    })

    function sleep (time) {
        return new Promise((resolve) => setTimeout(resolve, time))
    }



})