<template>
<h2>{{store.username}}</h2>
   <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="store.username"
        label="Username"
        hint="eg. Konrad"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please enter a username']"
      />

      <q-input
        filled
        v-model="store.password"
        type="password"
        label="Password"
        lazy-rules
        :rules="[
          val => val && val.length > 0 || 'Please enter a prefix for your container'
        ]"
      />

      <q-input
        filled
        v-model="store.course"
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

const PouchDB = require("pouchdb-browser").default;
//import Encryption from '../scripts/Encryption.js'
import store from '@/store'

//const $q = useQuasar()

/*const username = ref(null)
const password = ref(null)
const course = ref(null)*/

var db = new PouchDB("constellation");

db.get('user')
  .then((doc) => {
    console.log(doc)
    store.username.value = doc.username,
    store.password.value = doc.password,
    store.course.value = doc.course

  })
  .catch((e) => {console.log("Creating new user");console.log(e)})

function onSubmit () {
  saveUser(store.username.value, store.password.value, store.course.value, [{
        image : "hello-world",
        /*ports : {
          '8080' : 1000
        },*/
        prefix : "hello-world"
      }])
}
function saveUser(uname, pword, course, condef) 
{
  db.put({
    _id : "user",
    username : uname,
    //password : Encryption.SHA1(password.value),
    password : pword,
    course : course,
    container_definitions : condef
  })

//  db.sync('constellation', 'https://localhost:5984/mydb');

}
</script>

<style>

</style>