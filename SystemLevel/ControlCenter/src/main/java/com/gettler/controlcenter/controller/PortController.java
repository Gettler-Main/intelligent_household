package com.gettler.controlcenter.controller;

import com.gettler.controlcenter.mapper.PortMapper;
import com.gettler.controlcenter.mapper.UserMapper;
import com.gettler.controlcenter.pojo.Port;
import com.gettler.controlcenter.pojo.PortExample;
import com.gettler.controlcenter.pojo.User;
import com.gettler.controlcenter.pojo.UserExample;
import com.gettler.controlcenter.util.MybatisUtils;
import com.gettler.controlcenter.util.ShellPower;
import com.gettler.controlcenter.vo.Result;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiParam;
import lombok.SneakyThrows;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.io.IOException;
import java.util.List;

@RestController
@Api(tags = {"端口接口"})
@RequestMapping("Port")
public class PortController {

    PortMapper portMapper = MybatisUtils.getSqlseesion().getMapper(PortMapper.class);

    @GetMapping("addport")
    @ApiOperation(value = "添加端口")
    public Result addport(@ApiParam(name = "num", value = "端口号", required = true, example = "50001") Integer num, @ApiParam(name = "userid", value = "用户ID", required = true, example = "1") Integer userid) throws IOException {
        PortExample portExample = new PortExample();
        portExample.createCriteria().andUseridEqualTo(userid);
        List<Port> port = portMapper.selectByExample(portExample);
        if (port.isEmpty()) {
            int pid = ShellPower.addPort(num);
            portMapper.insert(new Port(userid, num, pid));
            return Result.success(false);
        }
        return Result.success("用户ID已存在");
    }

    @GetMapping("deleteport")
    @ApiOperation(value = "删除端口")
    public Result deleteport(@ApiParam(name = "userId", value = "用户ID", required = true, example = "1") Integer userId) {
        ShellPower.deletePort(portMapper.selectByPrimaryKey(userId).getPid());
        return Result.success(portMapper.deleteByPrimaryKey(userId));
    }

    @GetMapping("updateport")
    @ApiOperation(value = "更新端口号")
    public Result updateport(@ApiParam(name = "num", value = "端口号", required = true, example = "50001") Integer num, @ApiParam(name = "userid", value = "用户ID", required = true, example = "1") Integer userId, @ApiParam(name = "newnum", value = "新端口号", required = true, example = "50002") Integer newnum) throws IOException {
        PortExample portExample = new PortExample();
        portExample.createCriteria().andNumEqualTo(num).andUseridEqualTo(userId);
        List<Port> ports = portMapper.selectByExample(portExample);
        if (ports.isEmpty()) {
            return Result.success("原端口号错误");
        }
        ShellPower.deletePort(portMapper.selectByPrimaryKey(userId).getPid());
        int pid = ShellPower.addPort(newnum);
        return Result.success(portMapper.updateByExample(new Port(ports.get(0).getUserid(), newnum, pid), portExample));
    }

    @GetMapping("findAll")
    @ApiOperation(value = "查找所有用户及端口")
    public Result findAll() {
        return Result.success(portMapper.selectByExample(null));
    }

    @GetMapping("findPortByUserId")
    @ApiOperation(value = "通过用户Id查找端口号")
    public Result findPortByUserId(@ApiParam(name = "id", value = "用户ID", required = true, example = "1") Integer userid) {
        return Result.success(portMapper.selectByPrimaryKey(userid));
    }

}
