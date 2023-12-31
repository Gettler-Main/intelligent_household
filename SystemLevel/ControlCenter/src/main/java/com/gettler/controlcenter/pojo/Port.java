package com.gettler.controlcenter.pojo;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Port {
    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column port.userid
     *
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    private Integer userid;

    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column port.num
     *
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    private Integer num;

    /**
     * This field was generated by MyBatis Generator.
     * This field corresponds to the database column port.pid
     *
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    private Integer pid;

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column port.userid
     *
     * @return the value of port.userid
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public Integer getUserid() {
        return userid;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column port.userid
     *
     * @param userid the value for port.userid
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public void setUserid(Integer userid) {
        this.userid = userid;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column port.num
     *
     * @return the value of port.num
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public Integer getNum() {
        return num;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column port.num
     *
     * @param num the value for port.num
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public void setNum(Integer num) {
        this.num = num;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method returns the value of the database column port.pid
     *
     * @return the value of port.pid
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public Integer getPid() {
        return pid;
    }

    /**
     * This method was generated by MyBatis Generator.
     * This method sets the value of the database column port.pid
     *
     * @param pid the value for port.pid
     * @mbggenerated Sun May 08 16:19:53 CST 2022
     */
    public void setPid(Integer pid) {
        this.pid = pid;
    }
}