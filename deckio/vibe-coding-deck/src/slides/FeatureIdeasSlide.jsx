import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './FeatureIdeasSlide.module.css'

export default function FeatureIdeasSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.featureIdeasSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>07 — Feature Specs</p>
          <h1>Pick a Feature, Start Building</h1>
          <p className={styles.subtitle}>
            Ready-made specs in <code>feature-ideas/</code> — or invent your
            own.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <span className={styles.featureNumber}>01</span>
            <h3 className={styles.cardTitle}>
              Guest Reviews &amp; Ratings
            </h3>
            <p className={styles.cardText}>
              Let guests leave reviews and star ratings after their stay.
              Backend, frontend, and data changes are all spec&apos;d out.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.featureNumber}>02</span>
            <h3 className={styles.cardTitle}>
              Booking Modifications &amp; Upgrades
            </h3>
            <p className={styles.cardText}>
              Modify existing bookings and offer room upgrades. Covers
              availability checks, pricing logic, and UI flows.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.featureNumber}>03</span>
            <h3 className={styles.cardTitle}>
              Favorites &amp; Trip Planning
            </h3>
            <p className={styles.cardText}>
              Save hotels to favorites and build trip plans. Includes
              wishlists, itinerary views, and sharing capabilities.
            </p>
          </div>
        </div>

        <div className={styles.callout}>
          <p>
            <strong>How to use:</strong> Open a feature file → ask Copilot{' '}
            <em>&quot;implement the feature described in this file&quot;</em> →
            iterate and refine.
          </p>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
