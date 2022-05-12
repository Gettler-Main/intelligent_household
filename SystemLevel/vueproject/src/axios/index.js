// services/global.js"
import axios from 'axios'
import { Message, Loading } from 'element-ui'
let loadingInstance = null

// 初始化实例
let http = axios.create({
// 请求超时时间
  baseURL: '',
  timeout: 7000,
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded'
  }
})

http.interceptors.request.use((config) => {
  loadingInstance = Loading.service({
    lock: true,
    text: 'loading...'
  })
  return config
}, (error) => {
  return Promise.reject(error)
})

// 响应拦截器
http.interceptors.response.use((response) => {
  loadingInstance.close()
  return response
}, (error) => {
  console.log('TCL: error', error)
  const msg = error.Message !== undefined ? error.Message : ''
  Message({
    message: '网络错误' + msg,
    type: 'error',
    duration: 3 * 1000
  })
  loadingInstance.close()
  return Promise.reject(error)
})

// 封装axios的post请求
http.post = (url, params) => {
  return new Promise((resolve, reject) => {
    axios.post(url, params).then(response => {
      resolve(response.data)
    }).catch(err => {
      reject(err)
    })
  })
}
// 封装axios的apisPost请求
http.apisPost = (url, params) => {
  url = '/apis' + url
  return http.post(url, params)
}

// 封装axios的get请求
http.get = (url, params) => {
  return new Promise((resolve, reject) => {
    axios.get(url, params).then(response => {
      resolve(response.data)
    }).catch(err => {
      reject(err)
    })
  })
}
// 封装axios的apisGet请求
http.apisGet = (url, params) => {
  url = '/apis' + url
  return http.get(url, params)
}
//delete
// 封装axios的delete请求
http.delete = (url, params) => {
  return new Promise((resolve, reject) => {
    axios.delete(url, params).then(response => {
      resolve(response.data)
    }).catch(err => {
      reject(err)
    })
  })
}
// 封装axios的apisDelete请求
http.apisDelete = (url, params) => {
  url = '/apis' + url
  return http.delete(url, params)
}

//put
// 封装axios的put请求
http.put = (url, params) => {
  return new Promise((resolve, reject) => {
    axios.put(url, params).then(response => {
      resolve(response.data)
    }).catch(err => {
      reject(err)
    })
  })
}
// 封装axios的apisPut请求
http.apisPut = (url, params) => {
  url = '/apis' + url
  return http.put(url, params)
}

export default http
