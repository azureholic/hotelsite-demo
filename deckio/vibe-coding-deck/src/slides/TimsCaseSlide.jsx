import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './TimsCaseSlide.module.css'

export default function TimsCaseSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.timsCaseSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>04 — Real-World Example</p>
          <h1>Tim&apos;s Case — Shared on Teams</h1>
          <p className={styles.subtitle}>
            From idea to internal demo in one fast cycle using vibe coding
            practices.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 1</span>
            <h3 className={styles.cardTitle}>Problem Framing</h3>
            <p className={styles.cardText}>
              Defined the user pain point and success criteria in one
              prompt-driven brief using Copilot Chat.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 2</span>
            <h3 className={styles.cardTitle}>Rapid Build</h3>
            <p className={styles.cardText}>
              Used Agent mode + Copilot CLI to scaffold, implement, and test the
              core flow within a single sprint window.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 3</span>
            <h3 className={styles.cardTitle}>Team Feedback</h3>
            <p className={styles.cardText}>
              Shared the working demo on Teams. Comments became backlog items.
              Validation happened before over-engineering.
            </p>
          </div>
        </div>

        <div className={styles.takeaway}>
          <p>
            <strong>Key takeaway:</strong> Vibe coding didn&apos;t replace
            engineering judgment — it accelerated the path from concept to
            validated direction.
          </p>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
