<template>
  <h1>Dashboard - container</h1>
  <input type="button" value="Don't click me" @click="sendRequest"/>
</template>

<script setup>
import axios from 'axios'
function sendRequest() 
{
  axios.get("https://jsonplaceholder.typicode.com/todos/1")
    .then(response => console.log(response.data))
}


</script>

<style>

</style>