<template>
  
    <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="image"
        label="Docker image"
        hint="docker.data.techcollege.dk/[image]"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please enter a image name']"
      />

      <q-input
        filled
        v-model="prefix"
        label="Container prefix"
        lazy-rules
        :rules="[
          val => val && val.length > 0 || 'Please enter a prefix for your container'
        ]"
      />

      <q-input
        filled
        type="number"
        v-model="port"
        label="Container port"
        lazy-rules
        :rules="[
          val => val !== null && val !== '' || 'Enter a container port',
          val => val >= 0 && val <= 65535 || 'Enter a number between 0 and 65535'
        ]"
      />

      <div>
        <q-btn label="Submit" type="submit" color="primary"/>
        &nbsp;
        <q-btn label="Back" to="/" color="grey"/>
      </div>
    </q-form>
</template>

<script setup>

import { ref } from 'vue'
import store from '@/store'
import Database from '../scripts/Database.js'


const image = ref(null)
const port = ref(null)
const prefix = ref(null)

function onSubmit () {
  saveContainerDef()
    //if (accept.value !== true) {

    //}
    /*else {
        $q.notify({
        color: 'green-4',
        textColor: 'white',
        icon: 'cloud_done',
        message: 'Submitted'
        })
    }*/
}
function saveContainerDef()
{
  if (store.state.user.container_definitions == undefined) {
    store.state.user.container_definitions = [];
  }
  store.state.user.container_definitions.push(
    {
        image : image.value,
        prefix : prefix.value,
        port : parseInt(port.value)
      }
  );
  Database.saveData().then(
    () => {
      require("../router/index.js").default.push("/")
    }
  );
  
}


</script>

<style>

</style>