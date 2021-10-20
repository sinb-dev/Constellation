<template>
<q-page >
  <div class="q-pa-md q-gutter-md">

<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi vel magna a lorem bibendum viverra in et libero. Curabitur fringilla ullamcorper odio, sit amet gravida tortor ultricies at. Sed ac consectetur ante. Sed facilisis sem justo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur vitae dolor mauris. Fusce at lobortis magna. Curabitur ante leo, rhoncus et fringilla eget, tincidunt sit amet lacus. Quisque sodales ante nec orci volutpat, eu faucibus diam lobortis. Vivamus imperdiet diam et risus ornare, et mollis purus rutrum. Duis non tortor id neque ornare aliquet. Ut porta neque vel est laoreet, nec vulputate nulla congue. Nullam eu turpis nulla. Donec consequat est odio, posuere posuere est pellentesque in.</p>

<p>Nam scelerisque maximus tellus, quis varius odio congue ut. Cras nunc felis, maximus vel maximus ut, convallis tristique enim. Praesent dapibus elit at dolor aliquam pharetra. Nam vehicula congue dolor non rutrum. Nullam facilisis, nisl quis condimentum facilisis, augue sem auctor eros, eu dignissim urna lorem sit amet velit. Vivamus tristique massa in ullamcorper rutrum. Etiam at justo et libero auctor fringilla ac vel est. Etiam volutpat nec libero non pulvinar. Praesent hendrerit consectetur neque, vel tempus risus suscipit quis. Suspendisse nec sem ac enim consectetur ullamcorper quis id purus. Cras pharetra tellus ac egestas pellentesque. Suspendisse tempus justo ut cursus malesuada. Curabitur tincidunt lectus nec lectus elementum, sit amet pulvinar orci dapibus.</p>

<p>In semper, orci convallis vestibulum iaculis, leo dolor blandit metus, quis mattis est ante id nisi. Aenean vulputate sagittis leo, eget tincidunt nisi euismod vitae. Vestibulum non mollis libero. Duis a facilisis massa. Sed vel magna bibendum, placerat risus nec, commodo ante. Quisque a velit eu eros fermentum varius in non erat. Etiam viverra ipsum a sem luctus, non tincidunt tellus semper.</p>

<p>Vestibulum pharetra accumsan dolor a auctor. Aenean ornare dapibus maximus. In hac habitasse platea dictumst. Cras rhoncus eget metus non hendrerit. Suspendisse faucibus augue lectus, sed pretium elit pharetra vitae. Nam faucibus magna quis molestie porttitor. In et nibh et nulla tristique blandit. Aliquam venenatis nibh nec ligula finibus, in ornare quam aliquam. Suspendisse vitae lorem ligula.</p>

<p>Mauris et ipsum tincidunt, scelerisque quam ac, venenatis ipsum. In hac habitasse platea dictumst. Fusce vitae odio a urna ornare gravida. Maecenas ac auctor sapien. Donec et lacinia ligula. Mauris ut nisi efficitur, placerat purus a, vulputate nulla. Nullam sollicitudin ante molestie, accumsan nibh et, egestas diam. Curabitur dapibus laoreet lacus, eu tempor augue ornare eu. Quisque tempor accumsan ipsum, sed feugiat ipsum vestibulum sit amet. In hac habitasse platea dictumst. Nunc gravida suscipit ligula, sit amet facilisis elit aliquet id. Nulla purus massa, ornare sit amet metus aliquam, porta congue sapien. Nullam ullamcorper ac elit eget pulvinar. Cras eu lacus iaculis, rutrum lectus luctus, finibus magna. Pellentesque porta vitae ipsum ac vestibulum. Maecenas pellentesque maximus ipsum in maximus. </p>
    <q-list bordered padding class="rounded-borders" >
      <q-item-label header>Folders</q-item-label>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="view_in_ar" color="primary" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">Photos</q-item-label>
          <q-item-label caption>February 22nd, 2019</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="play_arrow" color="green" />
        </q-item-section>
      </q-item>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="folder" color="orange" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">Movies</q-item-label>
          <q-item-label caption>March 1st, 2019</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="info" />
        </q-item-section>
      </q-item>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="folder" color="teal" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">Photos</q-item-label>
          <q-item-label caption>January 15th, 2019</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="info" />
        </q-item-section>
      </q-item>

      <q-separator spaced />
      <q-item-label header>Files</q-item-label>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="assignment" color="grey" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">Expenses spreadsheet</q-item-label>
          <q-item-label caption>March 2nd, 2019</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="info" />
        </q-item-section>
      </q-item>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="place" color="grey" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">Places to visit</q-item-label>
          <q-item-label caption>February 22, 2019</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="info" color="amber" />
        </q-item-section>
      </q-item>

      <q-item clickable v-ripple>
        <q-item-section avatar top>
          <q-avatar icon="library_music" color="grey" text-color="white" />
        </q-item-section>

        <q-item-section>
          <q-item-label lines="1">My favorite song</q-item-label>
          <q-item-label caption>Singing it all day</q-item-label>
        </q-item-section>

        <q-item-section side>
          <q-icon name="info" />
        </q-item-section>
      </q-item>
    </q-list>
  </div>
</q-page>
</template>

<script setup>
//import axios from 'axios'
//import { ref } from 'vue'
import Constellation from '../scripts/Constellation'
Constellation.checkSetup()


</script>

<style>

</style>