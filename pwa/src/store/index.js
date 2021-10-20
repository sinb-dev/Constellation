import { createStore } from 'vuex'

export default createStore({
  state: {
    show_nav : false,
    username : "Anne_H4PD101121",
    password : "Dam",
    container_definitions : [
      {
        image : "possum",
        ports : {
          '8080' : 1000
        },
        prefix : "possum"
      },
      {
        image : "hello-world",
        ports : {
          '8080' : 1000
        },
        prefix : "possum"
      }
    ]
  },
  mutations: {
  },
  actions: {
  },
  modules: {
  }
})
