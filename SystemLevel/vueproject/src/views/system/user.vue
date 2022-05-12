<template>
  <div class="main-card">
    <div class="li-div" style="margin-bottom: 20px;">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>首页</el-breadcrumb-item>
        <el-breadcrumb-item>基础管理</el-breadcrumb-item>
        <el-breadcrumb-item>用户管理</el-breadcrumb-item>
      </el-breadcrumb>
      <el-divider></el-divider>
      <el-button
        type="primary"
        size="small"
        icon="el-icon-plus"
        @click="openUserModelAdd(null)"
      >
        新增用户
      </el-button>
    </div>
    <!-- 表格展示 -->
    <el-table
      border
      :data="roleList"
      tooltip-effect="dark"
      row-key="id"
      v-loading="loading"
      :header-cell-style="{background:'#c5c9d5',color:'#606266'}"
    >
      <!-- 表格列 -->
      <el-table-column prop="userid" label="角色ID" align="center" min-width="250"/>
      <el-table-column prop="username" label="用户名" align="center" min-width="250">
      </el-table-column>
      <el-table-column
        prop="password"
        label="密码"
        min-width="250"
        align="center"
      >
      </el-table-column>
      <!-- 列操作 -->
      <el-table-column label="操作" align="center" width="400">
        <template slot-scope="scope">
          <el-button
            type="text"
            size="mini"
            @click="openMenuModel(scope.row)"
          >
            <i class="el-icon-edit" /> 修改用户
          </el-button>

          <el-popconfirm
            class="prog"
            title="确定删除吗？"
            style="margin-left:10px"
            @confirm="deleteRoles(scope.row.userid)"
          >
            <el-button size="mini" type="text" slot="reference">
              <i class="el-icon-delete" /> 删除
            </el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>

    <!-- 菜单对话框 -->
    <el-dialog :visible.sync="roleMenu" width="30%">
      <!--<div class="dialog-title-container" slot="title" ref="roleTitle" />-->
      <div class="dialog-title-container" slot="title">修改用户</div>
      <el-form label-width="100px" size="medium" :model="roleForm" :rules="rule" ref="roleForm">
        <el-form-item label="用户名称">
          <el-input v-model="roleForm.username" style="width:250px" disabled/>
        </el-form-item>
        <el-form-item label="用户密码">
          <el-input v-model="roleForm.password" style="width:250px" disabled/>
        </el-form-item>
        <el-form-item label="新用户名称">
          <el-input v-model="roleForm.newUserName" style="width:250px"/>
        </el-form-item>
        <el-form-item label="新用户密码">
          <el-input v-model="roleForm.newPassword" style="width:250px"/>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button type="primary" @click="saveOrUpdateRoleMenu">
          确 定
        </el-button>
        <el-button @click="roleMenu = false">取 消</el-button>
      </div>
    </el-dialog>
    <!-- 新增对话框 -->
    <el-dialog :visible.sync="roleAdd" width="30%">
      <!--<div class="dialog-title-container" slot="title" ref="roleTitle" />-->
      <div class="dialog-title-container" slot="title">新增用户</div>
      <el-form label-width="100px" size="medium" :model="roleFormAdd" :rules="rule" ref="roleForm">
        <el-form-item label="用户名称">
          <el-input v-model="roleFormAdd.username" style="width:250px"/>
        </el-form-item>
        <el-form-item label="用户密码">
          <el-input v-model="roleFormAdd.password" style="width:250px"/>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button type="primary" @click="saveOrUpdateRoleUser">
          确 定
        </el-button>
        <el-button @click="roleAdd = false">取 消</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    name:"user",
    created() {
      this.listRoles();
    },
    data: function() {
      return {
        loading: true,
        isDelete: false,
        roleList: [],
        /*roleIdList: [],*/
        roleMenu: false,
        roleAdd: false,
        roleResource: false,
        roleRole: false,
        roleUser: false,
        roleForm: {
          username:"",
          password:"",
          newUserName:"",
          newPassword:"",
        },
        roleFormAdd:{
          username:"",
          password:"",
        },
        formGet: {
          username: "Gettler",
          password: "73748156"
        },
        rule: {
          newUserName: [
            { required: true, message: "用户名不能为空", trigger: "blur" }
          ],
          newPassword: [
            { required: true, message: "密码不能为空", trigger: "blur" }
          ]
        }
      };
    },
    methods: {
      listRoles() {
        this.$axios.get("http://47.93.12.205:8080/controlcenter/User/findAll", {
          params: {
          }
        })
          .then(res => {
            console.log(res)
            this.roleList  = res.data;
            this.loading = false;
          })

      },
      openMenuModel(roles) {
        if (roles != null) {
          this.roleForm = JSON.parse(JSON.stringify(roles));
        } else {
          this.roleForm = {
            roleName: "",
            roleLabel: "",
            resourceIdList: [],
            menuIdList: []
          };
        }
        /*this.roleForm = JSON.parse(JSON.stringify(roles));*/
        this.roleMenu = true;
      },
      saveOrUpdateRoleMenu() {
        if (this.roleForm.newUserName.trim() == "") {
          this.$message.error("新的用户名称不能为空");
          console.log('新的用户名称不能为空');
          return false;
        }
        if (this.roleForm.newPassword.trim() == "") {
          this.$message.error("新的用户密码不能为空");
          console.log('新的用户密码不能为空');
          return false;
        }
        this.$axios.get("http://47.93.12.205:8080/controlcenter/User/update", {
          params: {
            newPassword:this.roleForm.newPassword,
            newUserName:this.roleForm.newUserName,
            password:this.roleForm.password,
            username:this.roleForm.username,
          }
        }).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: res.message
            });
            this.listRoles();
          } else {
            this.$notify.error({
              title: "失败",
              message: res.message
            });
          }
          this.roleMenu = false;
        }).catch(() => {
          console.log('请求失败')
        })

      },
      //删除角色
      deleteRoles(id) {
        console.log(id);
        if(id ==36){
          this.$message.error("不可删除管理员");
          return false;
        }
        this.$axios.get("http://47.93.12.205:8080/controlcenter/User/delete",{
          params: {
            userId: id,
          }}
          ).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: "删除成功"
            });
            this.listRoles();
          } else {
            this.$notify.error({
              title: "失败",
              message: res.message
            });
          }
        }).catch(() => {
          console.log('请求失败')
        });
      },
      openUserModelAdd(roles) {
        if (roles != null) {
          this.roleFormAdd = JSON.parse(JSON.stringify(roles));
        } else {
          this.roleFormAdd = {
          };
        }
        this.roleAdd = true;
      },
      //新增角色post
      saveOrUpdateRoleUser() {
        if (this.roleFormAdd.password.trim() == "") {
          this.$message.error("密码不能为空");
          return false;
        }
        if (this.roleFormAdd.username.trim() == "") {
          this.$message.error("用户名不能为空");
          return false;
        }
        this.$axios.get("http://47.93.12.205:8080/controlcenter/User/register", {
          params: {
            password:this.roleFormAdd.password,
            username:this.roleFormAdd.username,
          }
        }).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: res.message
            });
            this.listRoles();
          } else {
            this.$notify.error({
              title: "失败",
              message: res.message
            });
          }
          this.roleAdd = false;
        }).catch(() => {
          console.log('请求失败')
        })
      },
    }
  };
</script>
<style>

  .li-div {
    margin-bottom: 20px;
  }
  .row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
    margin-top: 20px;
  }
  .pag-location {
    margin-top: 20px;
    margin-right: 30px;
  }
  .handle-input {
    width: 300px;
    display: inline-block;
    float: right;
  }
  .el-table /deep/ th.el-table-column--selection .cell {
    position: relative;
  }
  .el-table /deep/ th.el-table-column--selection .cell::before {
    position: absolute;
    right: 10px;
  }
  .notify-success{
    top: 1.7rem !important;
    right: 0.32rem !important;
    width: 2rem !important;
    height: 0.96rem !important;
    background: rgba(131, 213, 134, 0.1) !important;
    border-radius: 0.04rem 0px 0px 0.04rem !important;
  }
  .notify-error{
    top: 2.7rem !important;
    right: 0.32rem !important;
    width: 2rem !important;
    height: 0.96rem !important;
    background:  rgba(255, 31, 36, 0.05) !important;
    border-radius: 0.04rem 0px 0px 0.04rem !important;
  }

</style>
