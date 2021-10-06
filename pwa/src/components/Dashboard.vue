<template>
  <h1>Dashboard - container</h1>
  <input type="button" :value="label" @click="sendRequest"/>
  <input type="text" v-model="param" placeholder="Write message to server" />
</template>

<script setup>
import axios from 'axios'
import { ref } from 'vue'
var label = ref("CORS I RØVEN");
var param = ref("");

function sendRequest() 
{
  axios.get("http://localhost:5000/Container?myrequest="+param.value)
    .then(response => label.value = response.data.message)
}


</script>

<style>

</style>