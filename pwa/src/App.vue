<template>
  <div id="nav" v-if="store.state.show_nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
  </div>
  
  <router-view/>
</template>
<script setup>
//import { ref } from 'vue'
import store from '@/store'
//var show_nav = ref(store.state.show_nav)
//console.log(show_nav.value)
</script>

<style lang="scss">

</style>
