<template>
  <div class="main-card">
    <div class="li-div">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>首页</el-breadcrumb-item>
        <el-breadcrumb-item>系统管理</el-breadcrumb-item>
        <el-breadcrumb-item>端口管理</el-breadcrumb-item>
      </el-breadcrumb>
      <el-divider></el-divider>
      <el-button
        type="primary"
        size="small"
        icon="el-icon-plus"
        @click="openMenuModelAdd(null)"
      >
        新增端口
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
      <el-table-column prop="num" label="端口号" align="center" min-width="250"/>
      <el-table-column prop="pid" label="进程ID" align="center" min-width="250">
      </el-table-column>
      <el-table-column
        prop="userid"
        label="用户ID"
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
            @click="openPortModel(scope.row)"
          >
            <i class="el-icon-edit" /> 修改端口号
          </el-button>
          <el-popconfirm
            class="prog"
            title="确定删除吗？"
            style="margin-left:10px"
            @confirm="deletePort(scope.row.userid)"
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
      <el-form label-width="100px" size="medium" :model="rolePort">
        <el-form-item label="进程ID">
          <el-input v-model="rolePort.userid" style="width:250px" disabled/>
        </el-form-item>
        <el-form-item label="端口号">
          <el-input v-model="rolePort.num" style="width:250px" disabled/>
        </el-form-item>
        <el-form-item label="新的端口号">
          <el-input v-model="rolePort.newnum" style="width:250px"/>
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
      <div class="dialog-title-container" slot="title">新增</div>
      <el-form label-width="100px" size="medium" :model="rolePortAdd">
        <el-form-item label="用户ID">
          <el-input v-model="rolePortAdd.userid" style="width:250px"/>
        </el-form-item>
        <el-form-item label="端口号">
          <el-input v-model="rolePortAdd.num" style="width:250px"/>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button type="primary" @click="saveOrUpdateRoleMenuAdd">
          确 定
        </el-button>
        <el-button @click="roleAdd = false">取 消</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    name:'Variable',
    data: function() {
      return {
        loading: true,
        isDelete: false,
        roleList: [],
        /*roleIdList: [],*/
        keywords: null,
        current: 1,
        size: 10,
        count: 0,
        roleMenu: false,
        roleAdd: false,
        roleRole: false,
        roleUser: false,
        rolePort: {
          newnum:"",
          num:"",
          userid:"",
        },
        rolePortAdd:{
          num:"",
          userid:"",
        },
        roleDis: {
          id:null,
          isDisable:null
        },
      };
    },
    created() {
      this.listPort();
    },
    methods: {
      searchRoles() {
        this.current = 1;
        this.listRoles();
      },
      sizeChange(size) {
        this.size = size;
        this.listRoles();
      },
      currentChange(current) {
        this.current = current;
        this.listRoles();
      },
      selectionChange(roleList) {
        this.roleIdList = [];
        roleList.forEach(item => {
          this.roleIdList.push(item.id);
        });
      },
      listPort() {
        var that = this;
        this.$axios.get("http://47.93.12.205:8080/controlcenter/Port/findAll")
          .then(res => {
            console.log(res)
            this.roleList  = res.data;
            this.loading = false;
          })

      },
      openPortModel(roles) {
        /*this.$refs.roleTitle.innerHTML = roles ? "菜单权限" : "新增角色";*/
        if (roles != null) {
          this.rolePort = JSON.parse(JSON.stringify(roles));
        } else {
          this.rolePort = {
            roleName: "",
            roleLabel: "",
            resourceIdList: [],
            menuIdList: []
          };
        }
        this.roleMenu = true;
      },
      saveOrUpdateRoleMenu() {
        this.$axios.get("http://47.93.12.205:8080/controlcenter/Port/updateport", {
          params: {
            newnum:this.rolePort.newnum,
            num:this.rolePort.num,
            userid:this.rolePort.userid,
          }
        }).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: res.message
            });
            this.listPort();
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
      openMenuModelAdd(roles) {
        if (roles != null) {
          this.rolePortAdd = JSON.parse(JSON.stringify(roles));
        } else {
          this.rolePortAdd = {

          };
        }
        this.roleAdd = true;
      },
      //新增角色post
      saveOrUpdateRoleMenuAdd() {
        if (this.rolePortAdd.num.trim() == "") {
          this.$message.error("端口号不能为空");
          return false;
        }
        if (this.rolePortAdd.userid.trim() == "") {
          this.$message.error("用户ID不能为空");
          return false;
        }
        this.$axios.get("http://47.93.12.205:8080/controlcenter/Port/addport", {
          params: {
            num:this.rolePortAdd.num,
            userid:this.rolePortAdd.userid,
          }
        }).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: res.message
            });
            this.listPort();
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
      deletePort(id) {
        console.log(id)
        this.$axios.get("http://47.93.12.205:8080/controlcenter/Port/deleteport",{
          params: {
            userId:id,
          }
        }).then(res => {
          if (res.msg) {
            this.$notify.success({
              title: "成功",
              message: "删除成功"
            });
            this.listPort();
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
