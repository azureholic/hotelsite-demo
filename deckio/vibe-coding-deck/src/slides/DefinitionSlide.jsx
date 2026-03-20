import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './DefinitionSlide.module.css'

export default function DefinitionSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.definitionSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />
      <div className={`orb ${styles.orb3}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>01 — Definition</p>
          <h1>
            What Is <span className={styles.highlight}>Vibe Coding</span>?
          </h1>
          <p className={styles.subtitle}>
            You describe intent in natural language — the AI writes the first
            draft. You review, refine, and ship. Momentum replaces ceremony.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Intent Over Syntax</h3>
            <p className={styles.cardText}>
              Start from the outcome, not the boilerplate. Prompts express what
              you want built; the AI handles the how.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Human in the Loop</h3>
            <p className={styles.cardText}>
              AI drafts the first pass. You own architecture decisions,
              correctness, security reviews, and edge cases.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Fast Feedback Cycles</h3>
            <p className={styles.cardText}>
              Every commit, test run, and demo gives signal. Speed to learning
              is the real superpower.
            </p>
          </div>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
