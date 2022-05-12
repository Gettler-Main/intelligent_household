// 导入组件
import Vue from 'vue';
import Router from 'vue-router';
// 登录
import login from "../views/login";
// 首页
import index from '@/views/index';
// 用户管理
import user from "../views/system/user";
// 系统环境变量
import Variable from "../views/system/Variable";
// 启用路由
Vue.use(Router);

// 导出路由
export default new Router({
    routes: [
      {
        path: '/',
        name: 'login',
        component: login,
     },
      {
        path: '/index',
        name: '首页',
        component: index,
        iconCls: 'el-icon-tickets',
        children: [  {
            path: '/system/user',
            name: '用户管理',
            component: user,
            meta: {
                requireAuth: true
            }
        }, {
            path: '/system/Variable',
            name: '系统环境变量',
            component: Variable,
            meta: {
                requireAuth: true
            }
        },   ]
    }]
})
