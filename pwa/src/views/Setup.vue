<template>
   <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="username"
        label="Username"
        hint="eg. Konrad"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please enter a username']"
      />

      <q-input
        filled
        v-model="password"
        type="password"
        label="Password"
        lazy-rules
        :rules="[
          val => val && val.length > 0 || 'Please enter a prefix for your container'
        ]"
      />

      <q-input
        filled
        v-model="course"
        label="Course"
        hint="eg. H3PD011121"
        lazy-rules
        :rules="[
          val => val !== null && val !== '' || 'Enter your course name',
        ]"
      />

      <div>
        <q-btn label="Login" type="submit" color="primary"/>
      </div>
    </q-form>
</template>

<script setup>
//import { useQuasar } from 'quasar'
import { ref } from 'vue'
const PouchDB = require("pouchdb-browser").default;
//import store from '@/store'

//const $q = useQuasar()

const username = ref(null)
const password = ref(null)
const course = ref(null)

var db = new PouchDB("constellation");
function onSubmit () {
  db.put({
    _id : "user",
    username : username.value,
    password : password.value,
    course : course.value
  })

//  db.sync('constellation', 'https://localhost:5984/mydb');

}
</script>

<style>

</style>