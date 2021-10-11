<template>
  <h1>Dashboard - container</h1>
  docker.data.techcollege.dk/<select v-model="image">
    <option>possum</option>
    <option>hello-world</option>
  </select>
  <input type="button" value="Run container" @click="runContainer"/>
  <input type="number" v-model="port" placeholder="Internal portnumber" />
  <input type="text" v-model="userId" placeholder="Your ID" />
</template>

<script setup>
import axios from 'axios'
import { ref } from 'vue'
//var label = ref("CORS I RØVEN");
var image = ref("");
var port = ref(0);
var userId = ref("");

function runContainer() 
{
  console.log(image.value)
  console.log(port.value)
  
  axios.get("http://localhost:5000/Container?&userId="+userId.value
      +"image="+image.value
      +"&port="+port.value)
    .then(response => console.log(response.data.id))
}


</script>

<style>

</style>