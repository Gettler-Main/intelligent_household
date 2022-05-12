package com.gettler.controlcenter.controller;

import com.gettler.controlcenter.mapper.PortMapper;
import com.gettler.controlcenter.mapper.UserMapper;
import com.gettler.controlcenter.pojo.Port;
import com.gettler.controlcenter.pojo.User;
import com.gettler.controlcenter.pojo.UserExample;
import com.gettler.controlcenter.util.MapToObj;
import com.gettler.controlcenter.util.MybatisUtils;
import com.gettler.controlcenter.util.ShellPower;
import com.gettler.controlcenter.vo.Result;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiParam;
import lombok.SneakyThrows;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.util.List;
import java.util.Map;

@RestController
@Api(tags = {"用户接口"})
@RequestMapping("User")
@CrossOrigin
public class UserController {
    UserMapper userMapper = MybatisUtils.getSqlseesion().getMapper(UserMapper.class);
    PortMapper portMapper = MybatisUtils.getSqlseesion().getMapper(PortMapper.class);

    @GetMapping("check")
    @ApiOperation(value = "检查用户名密码")
    public Result check(@ApiParam(name = "username", value = "用户名", required = true, example = "Gettler") String username, @ApiParam(name = "password", value = "密码", required = true, example = "73748156") String password) throws IOException {
        UserExample userExample = new UserExample();
        userExample.createCriteria().andUsernameEqualTo(username).andPasswordEqualTo(password);
        List<User> user = userMapper.selectByExample(userExample);
        if (user.isEmpty()) {
            return Result.fail(402, "用户名密码错误");
        }
//        int userid = user.get(0).getUserid();
//        Port port = portMapper.selectByPrimaryKey(userid);
//        int pid = ShellPower.addPort(port.getNum());
        return Result.success(user.get(0).getUserid());
    }

    @GetMapping("register")
    @ApiOperation(value = "注册")
    public Result register(@ApiParam(name = "username", value = "用户名", required = true, example = "Gettler") String username, @ApiParam(name = "password", value = "密码", required = true, example = "73748156") String password) {
        UserExample userExample = new UserExample();
        userExample.createCriteria().andUsernameEqualTo(username);
        List<User> user = userMapper.selectByExample(userExample);
        if (user.isEmpty()) {
            userMapper.insert(new User(username, password));
            user = userMapper.selectByExample(userExample);
            int userId = user.get(0).getUserid();
            return Result.success(userId);
        }
        return Result.fail(402, "用户名重复");
    }

    @GetMapping("delete")
    @ApiOperation(value = "删除用户")
    public Result delete(@ApiParam(name = "userId", value = "用户ID", required = true, example = "1") Integer userId) {
        return Result.success(userMapper.deleteByPrimaryKey(userId));
    }

    @GetMapping("update")
    @ApiOperation(value = "更新用户信息")
    public Result update(@ApiParam(name = "username", value = "用户名", required = true, example = "Gettler") String username, @ApiParam(name = "password", value = "密码", required = true, example = "73748156") String password, @ApiParam(name = "newUserName", value = "新用户名", required = true, example = "Gettler") String newUserName, @ApiParam(name = "newPassword", value = "新密码", required = true, example = "73748156") String newPassword) {
        UserExample userExample = new UserExample();
        userExample.createCriteria().andUsernameEqualTo(username).andPasswordEqualTo(password);
        List<User> users = userMapper.selectByExample(userExample);
        if (users.isEmpty()) {
            return Result.fail(402, "原密码错误");
        }
        return Result.success(userMapper.updateByExample(new User(users.get(0).getUserid(), newUserName, newPassword), userExample));
    }

    @GetMapping("findAll")
    @ApiOperation(value = "查找所有用户")
    public Result findAll() {
        return Result.success(userMapper.selectByExample(null));
    }

    @GetMapping("findUserById")
    @ApiOperation(value = "通过Id查找用户")
    public Result findUserById(@ApiParam(name = "id", value = "用户ID", required = true, example = "1") Integer id) {
        return Result.success(userMapper.selectByPrimaryKey(id));
    }
}
