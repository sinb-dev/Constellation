import { createStore } from 'vuex'

export default createStore({
  state: {
    show_nav : false,
    container_definitions : [
      {
        image : "possum",
        ports : {
          '8080' : 1000
        },
        prefix : ""
      },
      {
        image : "hello-world",
        ports : {
          '8080' : 1000
        },
        prefix : ""
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
