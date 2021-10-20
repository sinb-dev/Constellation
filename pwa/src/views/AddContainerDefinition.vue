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
        v-model="age"
        label="Container port"
        lazy-rules
        :rules="[
          val => val !== null && val !== '' || 'Enter a container port',
          val => val >= 0 && val <= 65535 || 'Enter a number between 0 and 65535'
        ]"
      />

      <div>
        <q-btn label="Submit" type="submit" color="primary"/>
        <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
      </div>
    </q-form>

</template>

<script setup>

import { useQuasar } from 'quasar'
import { ref } from 'vue'

const $q = useQuasar()

const name = ref(null)
const age = ref(null)
const accept = ref(false)

function onSubmit () {
    if (accept.value !== true) {
        $q.notify({
        color: 'red-5',
        textColor: 'white',
        icon: 'warning',
        message: 'You need to accept the license and terms first'
        })
    }
    else {
        $q.notify({
        color: 'green-4',
        textColor: 'white',
        icon: 'cloud_done',
        message: 'Submitted'
        })
    }
}

function onReset () {
    name.value = null
    age.value = null
    accept.value = false
    
}


</script>

<style>

</style>