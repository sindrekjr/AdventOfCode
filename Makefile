define newline


endef

LEXT ?= cs
YEAR ?= $(shell date +%Y)
DAYS ?= $(shell seq 1 $(shell if [ $(YEAR) -ge 2025 ]; then echo 12; else echo 25; fi))

TEMPLATE := $(subst <YEAR>,$(YEAR),$(subst $(newline),\n,$(file < templates/solution.$(LEXT).template)))

ifeq ($(LEXT), cs)
	DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions/Year$(YEAR)"
else ifeq ($(LEXT), rs)
	DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions.Rust/src/y$(YEAR)"
else ifeq ($(LEXT), zig)
	DIRECTORY := "$(shell pwd)/AdventOfCode.Solutions.Zig/src/y$(YEAR)"
endif

solutions-files: $(DAYS)

$(DAYS):
	$(eval DAY=$(shell printf "%02d" $@))
ifeq ($(LEXT), cs)
	$(eval DAY_DIR="$(DIRECTORY)/Day$(DAY)")
	$(eval DAY_FILE="$(DAY_DIR)/Solution.cs")
else ifeq ($(LEXT), rs)
	$(eval DAY_DIR="$(DIRECTORY)")
	$(eval DAY_FILE="$(DIRECTORY)/d$(DAY).rs")
else ifeq ($(LEXT), zig)
	$(eval DAY_DIR="$(DIRECTORY)")
	$(eval DAY_FILE="$(DIRECTORY)/d$(DAY).zig")
endif
	@if [ ! -f $(DAY_FILE) ]; then \
		mkdir -p $(DAY_DIR); \
		echo '$(subst <DAY>,$(DAY),$(TEMPLATE))' > $(DAY_FILE); \
	fi
