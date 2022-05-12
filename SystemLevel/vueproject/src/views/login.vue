<!--
<template>
  <div class="login-container">
    <div class="login-card">
      <div class="login-title">管理员登录</div>
      &lt;!&ndash; 登录表单 &ndash;&gt;
      <el-form
        status-icon
        :model="loginForm"
        :rules="rules"
        ref="ruleForm"
        class="login-form"
      >
        &lt;!&ndash; 用户名输入框 &ndash;&gt;
        <el-form-item prop="username">
          <el-input
            v-model="loginForm.username"
            prefix-icon="el-icon-user-solid"
            placeholder="用户名"
            @keyup.enter.native="login"
          />
        </el-form-item>
        &lt;!&ndash; 密码输入框 &ndash;&gt;
        <el-form-item prop="password">
          <el-input
            v-model="loginForm.password"
            prefix-icon="iconfont el-icon-mymima"
            show-password
            placeholder="密码"
            @keyup.enter.native="login"
          />
        </el-form-item>
      </el-form>
      &lt;!&ndash; 登录按钮 &ndash;&gt;
      <el-button type="primary" @click="login">登录</el-button>
    </div>
  </div>
</template>

<script>
  export default {
    name:"login",
    data: function() {
      return {
        loginForm: {
          username: "",
          password: ""
        },
        rules: {
          username: [
            { required: true, message: "用户名不能为空", trigger: "blur" }
          ],
          password: [{ required: true, message: "密码不能为空", trigger: "blur" }]
        }
      };
    },
    methods: {
      login() {
        this.$axios({
          method: "get",
          url: "/User/findAll",
          contentType: "application/json;chart=utf-8",
          dataType: "json",
          data,
        }).then((res) => {
          if (res.data.status === "1") {
            this.$Message.info("登录成功！");
            this.logining = false;

          } else {
            this.$Message.info("登录失败！");
            this.logining = false;

          }
        });

        /*this.$refs.ruleForm.validate(valid => {
          if (valid) {
            const that = this;
            // eslint-disable-next-line no-undef
            var captcha = new TencentCaptcha(
              this.config.TENCENT_CAPTCHA,
              function(res) {
                if (res.ret === 0) {
                  //发送登录请求
                  let param = new URLSearchParams();
                  param.append("username", that.loginForm.username);
                  param.append("password", that.loginForm.password);
                  that.axios.post("", param).then(({ data }) => {
                    if (data.flag) {
                      // 登录后保存用户信息
                      that.$store.commit("login", data.data);
                      // 加载用户菜单
                      generaMenu();
                      that.$message.success("登录成功");
                      that.$router.push({ path: "/" });
                    } else {
                      that.$message.error(data.message);
                    }
                  });
                }
              }
            );
            // 显示验证码
            captcha.show();
          } else {
            return false;
          }
        });*/
      }
    }
  };
</script>

<style scoped>
  .login-container {
    position: absolute;
    top: 10px;
    right: 0;
    bottom: 10px;
    width: 100%;
    background: url("../assets/img/preview.jpg") no-repeat right;

    background-size: cover;
  }
  .login-card {
    position: absolute;
    top: 0;
    bottom: 0;
    right: 0;
    background: #fff;
    padding: 170px 60px 180px;
    width: 350px;
  }
  .login-title {
    color: #303133;
    font-weight: bold;
    font-size: 1rem;
  }
  .login-form {
    margin-top: 1.2rem;
  }
  .login-card button {
    margin-top: 1rem;
    width: 100%;
  }
</style>
-->
<template>
  <el-container>
    <el-container >
      <div class="login-aside"></div>
      <div class="login-main">
        <!-- 登录表单 -->
        <el-form ref="form" :model="form" :rules="rules" class="login-form">
          <h2 class="login-title">用 户 登 录</h2>
          <!-- 用户名输入框-->
          <el-form-item prop="username">
            <el-input
              v-model="form.username"
              prefix-icon="el-icon-user-solid"
              placeholder="用户名"
              @keyup.enter.native="onSubmit"
            />
          </el-form-item>

          <!-- 密码输入框-->
          <el-form-item prop="password">
            <el-input
              v-model="form.password"
              prefix-icon="el-icon-lock"
              show-password
              placeholder="密码"
              @keyup.enter.native="onSubmit"
            />
          </el-form-item>
          <el-button type="primary" @click="onSubmit">登录</el-button>
          <div class="login-ad">
            <a @click="passHint">忘记密码？</a>
          </div>
        </el-form>
      </div>
    </el-container>
  </el-container>
</template>

<script>


  export default {
    name:'login',
    data () {
      return {
        form: {
          username: "",
          password: ""
        },
        formGet: {
          username: "admin",
          password: "123456"
        },
        rules: {
          username: [
            { required: true, message: "用户名不能为空", trigger: "blur" }
          ],
          password: [
            { required: true, message: "密码不能为空", trigger: "blur" }
          ]
        }
      }
    },
    methods: {
      onSubmit () {
            var that = this;
           /* let param = new URLSearchParams();
            param.append("username", that.form.username);
            param.append("password", that.form.password);*/
           /*Gettler   73748156*/
            if(this.form.username ==this.formGet.username&&this.form.password ==this.formGet.password){
              that.$router.push({ path: "/index" });
              sessionStorage.setItem("loginName", this.form.username);
              /*that.$axios.get("http://47.93.12.205:8080/controlcenter/User/check",{
                  params: {
                    password: this.form.password,
                    username: this.form.username
                  }
                }
              ).then(res => {
                if (res.msg) {
                  /!*that.$message.success("登录成功");*!/
                  that.$router.push({ path: "/index" });
                  sessionStorage.setItem("loginName", this.form.username);
                  sessionStorage.setItem("userInfo", this.form.username)
                } else {
                  alert('用户名或密码出错！')
                }
              }).catch(() => {
                alert('用户名或密码出错！')
              })*/
            }
            else{
              alert('用户名或密码出错！')
            }






        /*var that = this
        //发送登录请求
        const params = new URLSearchParams()
        params.append('username', that.form.username)
        params.append('password', that.form.password)
        this.$axios.get('http://47.93.12.205:8080/controlcenter/User/check', params)
          .then(res => {
            console.log(res.data)
            //localStorage.setItem("token", res.data.jwtToken) // 将jwtToken存到本地存储中
            sessionStorage.setItem("token", res.data.jwtToken)
            if (res.flag) {
              this.loginUserSave(res.data.id)
              this.$router.push('/index')	//登录验证成功路由实现跳转
            } else {
              this.$notify({
                title: '提示',
                message: '用户登录失败',
                duration: 3000
              })
            }
          })*/
      },
      passHint(){
        this.$alert('请联系管理员15345782156为您找回密码！', '忘记密码？', {
          confirmButtonText: '确定',
          type:"warning"
        });
      }
    }
  };
</script>

<style scoped>
  .login-aside {
    position: absolute;
    width: 65%;
    top: 0;
    bottom: 0;
    background: url("../assets/img/preview.jpg") no-repeat center center;
    background-size: cover;
  }
  .login-main {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    width: 35%;
    background: url("../assets/img/bg.png") no-repeat center center;
    /*background-color:#333333;*/
    background-size: cover;
  }
  .login-form {
    padding: 90px 30px;
    width: 300px;
    margin: 25px auto 20px;
  }
  .login-title {
    color: white;
    text-align: center;
    padding-bottom: 8px;
  }
  .el-form-item {
    margin: 30px auto;
  }
  .login-form button {
    width: 100%;
    background-color: #e7b330;
    border: #deab2d;
  }
  .login-form button:hover {
    background-color: orange;
  }
  .login-ad {
    margin-top: 15px;
    padding-bottom: 6px;
  }
  a {
    margin-top: auto;
    float: right;
    font-size: 14px;
    color: orange;
  }
  a:hover {
    color: gold;
  }
</style>
